using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CarGo.Data;
using CarGo.Game.Controller;
using CarGo.Game.Function;
using CarGo.Game.Player;
using CarGo.Menu;
using CarGo.Scene;
using UnityEngine;

namespace CarGo.Game
{
    public class GameManager : MonoBehaviour
    {
        #region SINGLETON

        private static GameManager _instance;

        public static GameManager Instance => _instance;

        private void Awake()
        {
            if (_instance is null)
            {
                _instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        #endregion

        private const float TileDistanceHorizontal = 3.5f;
        private const float TileDistanceVertical = 1.75f;
        
        [SerializeField] private Tile firstTile;
        [SerializeField] private List<Tile> Tiles = new List<Tile>();
        
        [SerializeField] private CameraController cameraController;
        [SerializeField] private FunctionManager _functionManager;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Vector3 moveDirection;
        [SerializeField] private ActionController actionController;
        [SerializeField] private CompleteController completeController;
        [SerializeField] private SceneManager sceneManager;
        
        private CancellationTokenSource _cancellationToken;
        private Quaternion defaultRotation;
        private List<IObjective> objectives;
        
        private void Start()
        {
            if(Tiles.Count == 0)
                Tiles = new List<Tile>(FindObjectsOfType<Tile>());

            objectives = new List<IObjective>();

            foreach (Tile tile in Tiles)
            {
                if(tile is IObjective objective)
                {
                    objectives.Add(objective);
                    objective.OnComplete += CheckObjective;
                }

                if (firstTile is null && tile is FirstTile)
                    firstTile = tile;
            }
            
            CenterCamera();
            _functionManager.Initialize();

            if (_playerController is null)
            {
                _playerController = FindObjectOfType<PlayerController>();
            }

            defaultRotation = _playerController.transform.rotation;
            Reset();
            _cancellationToken = null;

            actionController.PlayButton.ButtonDown += Play;
            actionController.ResetButton.ButtonDown += _functionManager.ResetFunction;

            completeController.ContinueButton.ButtonDown += () => sceneManager.ChangeScene(Path.ScenePath.MainMenu);
            
            Debug.Log("Game Start");
        }

        public ITile NextTile(ITile currentTile, Vector3 direction)
        {
            if (Tiles.Count == 0)
            {
                return null;
            }

            foreach (Tile tile in Tiles)
            {
                Vector3 _direction = currentTile.Position - tile.Position;
                _direction.y = 0;
                _direction.Normalize();

                if (_direction == direction * -1f && Vector3.Distance(currentTile.Position, tile.Position) <= TileDistanceHorizontal)
                {
                    return tile;
                }
            }

            return null;
        }
        
        public ITile NextTile(ITile currentTile, Vector3 direction, bool jump)
        {
            if (Tiles.Count == 0)
            {
                return null;
            }

            foreach (Tile tile in Tiles)
            {
                Vector3 newPos = tile.Position;
                newPos.y = 0;
                Vector3 currentPos = currentTile.Position;
                currentPos.y = 0;
                Vector3 _direction = currentPos - newPos;
                _direction.Normalize();

                if (_direction == direction * -1f && Vector3.Distance(newPos, currentPos) <= TileDistanceHorizontal && Math.Abs(currentTile.Position.y - tile.Position.y) <= TileDistanceVertical)
                {
                    return tile;
                }
            }

            return null;
        }

        private void CenterCamera()
        {
            Vector3 topLeft = new Vector3(float.MaxValue, 0, float.MinValue);
            Vector3 bottomRight = new Vector3(float.MinValue, 0, float.MaxValue);

            if (Tiles.Count == 0)
            {
                
            }
            
            foreach (Tile tile in Tiles)
            {
                if (tile.Position.x < topLeft.x)
                    topLeft.x = tile.Position.x;
                if (tile.Position.z > topLeft.z)
                    topLeft.z = tile.Position.z;
                if (tile.Position.x > bottomRight.x)
                    bottomRight.x = tile.Position.x;
                if (tile.Position.z < bottomRight.z)
                    bottomRight.z = tile.Position.z;
            }

            float distance = Vector3.Distance(bottomRight, topLeft) / 2f;

            Vector3 direction = bottomRight - topLeft;
            
            direction.Normalize();

            cameraController.LookPosition.position = topLeft + (direction * distance);
        }

        private async void Play()
        {
            if (_cancellationToken is null)
            {
                actionController.PlayButton._Image.color = new Color(0.5f, 0.5f, 0.5f);
                actionController.ResetButton._Image.raycastTarget = false;
                _cancellationToken = new CancellationTokenSource();
                CancellationToken ct = _cancellationToken.Token;
                _functionManager.ActiveRaycast(false);
                
                foreach (var mainCommand in _functionManager.CommandList(FunctionType.Main))
                {
                    if (ct.IsCancellationRequested)
                        break;
                    
                    CommandType current = mainCommand.CommandType;

                    if (current == CommandType.Procedure)
                    {
                        while (current == CommandType.Procedure)
                        {
                            foreach (var procedureCommand in _functionManager.CommandList(FunctionType.Procedure))
                            {
                                if (ct.IsCancellationRequested)
                                    break;
                                current = procedureCommand.CommandType;
                                await _playerController.ExecuteCommand(current, ct);
                            }
                        }
                    }else
                        await _playerController.ExecuteCommand(current, ct);
                }
            }
            else
            {
                _cancellationToken?.Cancel();
                _cancellationToken = null;
                Reset();
            }
        }

        private void Reset()
        {
            foreach (IObjective objective in objectives)
            {
                if (objective.Completed)
                {
                    objective.Reset();
                }
            }
            
            _playerController.transform.position = firstTile.Position;
            _playerController.transform.rotation = defaultRotation;
            _playerController.MoveDirection = moveDirection;
            _playerController.CurrentTile = firstTile;

            _functionManager.ActiveRaycast(true);
            actionController.ResetButton._Image.raycastTarget = true;
            actionController.PlayButton._Image.color = new Color(1f, 1f, 1f);
        }

        private async void CheckObjective()
        {
            foreach (IObjective objective in objectives)
            {
                if(!objective.Completed)
                    return;
            }
            
            _cancellationToken?.Cancel();

            await new WaitForSeconds(.2f);
            DataManager.SaveData((int)SceneManager.CurrentScene);
            Debug.Log("Game Compelete");
            completeController.Show();
        }

        private void OnDestroy()
        {
            _instance = null;
        }
    }
}
