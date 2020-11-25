using System.Collections.Generic;
using UnityEngine;

namespace Title.Game
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

        [SerializeField] private Tile firstTile;
        [SerializeField] public List<Tile> Tiles = new List<Tile>();
        
        [SerializeField] private CameraController cameraController;

        private void Start()
        {
            if (firstTile is null && Tiles.Count > 0)
            {
                firstTile = Tiles[0];
            }

            Vector3 topLeft = Vector3.zero;
            Vector3 bottomRight = Vector3.zero;

            foreach (Tile tile in Tiles)
            {
                if (tile.Position.x < topLeft.x)
                    topLeft.x = tile.Position.x;
                if (tile.Position.z > topLeft.z)
                    topLeft.z = tile.Position.z;
                if (tile.Position.x > bottomRight.x)
                    bottomRight.x = tile.Position.x;
                if (tile.Position.z < bottomRight.z)
                    topLeft.z = tile.Position.z;
            }

            float distance = Vector3.Distance(bottomRight, topLeft) / 2f;

            Vector3 direction = bottomRight - topLeft;
            
            direction.Normalize();

            cameraController.LookPosition.position = topLeft + (direction * distance);
        }

        public Vector3? NextTile(Vector3 position, Vector3 direction)
        {
            if (Tiles.Count == 0)
            {
                return null;
            }

            position.y = 0;

            foreach (Tile tile in Tiles)
            {
                Vector3 _direction = position - tile.Position;
                _direction.y = 0;
                _direction.Normalize();

                if (_direction == direction * -1f && Vector3.Distance(position, tile.Position) < 2f)
                {
                    return tile.Position;
                }
            }

            return null;
        }
    }
}
