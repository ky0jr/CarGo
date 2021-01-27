using System;
using System.Threading;
using System.Threading.Tasks;
using CarGo.Game.Function;
using UnityEngine;

namespace CarGo.Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        public Vector3 MoveDirection { private get; set; }

        public ITile CurrentTile { get; set; }

        private const float Speed = 1.25f;

        private const float RotationSpeed = 240f;

        public Task CurrentTask { private set; get; } = Task.CompletedTask;

        public async Task ExecuteCommand(CommandType commandType, CancellationToken cancellationToken)
        {
            if(!CurrentTask.IsCompleted && !CurrentTask.IsCanceled)
                return;
            
            switch (commandType)
            {
                case CommandType.Move:
                    CurrentTask = Move(cancellationToken);
                    break;
                case CommandType.RotateLeft:
                case CommandType.RotateRight:
                    CurrentTask = Rotate(commandType, cancellationToken);
                    break;
                case CommandType.LightUp:
                    CurrentTask = LightUp(cancellationToken);
                    break;
                case CommandType.BoostUp:
                    CurrentTask = BoostUp(cancellationToken);
                    break;
                default: return;
            }

            await CurrentTask;
        }

        #region Command

        private async Task Rotate(CommandType commandType, CancellationToken cancellationToken)
        {
            Vector3 direction = commandType == CommandType.RotateLeft ? Vector3.down : Vector3.up;

            var rotation = (transform.rotation * Quaternion.Euler(0, 90 * direction.y, 0));

            await new WaitWhile(() =>
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return false;
                }
                
                if (Quaternion.Angle(transform.rotation, rotation) > 0.1f)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, RotationSpeed * Time.deltaTime);
                    return true;
                }

                transform.rotation = rotation;
                return false;
            });

            if (direction == Vector3.up)
            {
                if (MoveDirection == Vector3.right)
                    MoveDirection = Vector3.back;
                else if (MoveDirection == Vector3.back)
                    MoveDirection = Vector3.left;
                else if (MoveDirection == Vector3.left)
                    MoveDirection = Vector3.forward;
                else if(MoveDirection == Vector3.forward)
                    MoveDirection = Vector3.right;
                
            }
            else if (direction == Vector3.down)
            {
                if (MoveDirection == Vector3.left)
                    MoveDirection = Vector3.back;
                else if (MoveDirection == Vector3.forward)
                    MoveDirection = Vector3.left;
                else if (MoveDirection == Vector3.right)
                    MoveDirection = Vector3.forward;
                else if(MoveDirection == Vector3.back)
                    MoveDirection = Vector3.right;
            }
        }
        
        private async Task Move (CancellationToken cancellationToken)
        {
            ITile destination = GameManager.Instance.NextTile(CurrentTile, MoveDirection);
            
            if (!(destination is null))
            {
                await new WaitWhile(() =>
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return false;
                    }
                    if (Vector3.Distance(transform.position, destination.Position) > 0.1f)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, destination.Position, Speed * Time.fixedDeltaTime);
                        return true;
                    }

                    transform.position = destination.Position;
                    return false;
                });

                CurrentTile = destination;
            }
            else
            {
                await new WaitForFixedUpdate();
            }
        }

        private async Task LightUp(CancellationToken cancellationToken)
        {
            if (CurrentTile is IObjective objective)
            {
                await objective.Objective();
            }
        }
        
        private async Task BoostUp (CancellationToken cancellationToken)
        {
            ITile destination = GameManager.Instance.NextTile(CurrentTile, MoveDirection, true);
            float yPos = transform.position.y;

            if (!(destination is null))
            {
                await new WaitWhile(() =>
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return false;
                    }
                    
                    if (Vector3.Distance(transform.position, destination.Position) > 0.1f)
                    {
                        if (yPos < destination.Position.y)
                        {
                            if (Math.Abs(transform.position.y - destination.Position.y) > 0.1f)
                            {
                                Vector3 newPos = transform.position;
                                newPos.y = destination.Position.y;
                                
                                transform.position = Vector3.MoveTowards(transform.position, newPos, Speed * Time.fixedDeltaTime);
                            }
                            else
                            {
                                transform.position = Vector3.MoveTowards(transform.position, destination.Position, Speed * Time.fixedDeltaTime);
                            }
                        }
                        else
                        {
                            Vector3 fowardPos = destination.Position;
                            fowardPos.y = yPos;

                            Vector3 holdPos = destination.Position;
                            holdPos.y = 0;

                            Vector3 playerTargetPosition = transform.position;
                            playerTargetPosition.y = 0;
                            
                            if (Math.Abs(transform.position.y - destination.Position.y) > 0.1f && Vector3.Distance(holdPos, playerTargetPosition) <= 0.1f)
                            {
                                transform.position = Vector3.MoveTowards(transform.position, destination.Position, Speed * Time.fixedDeltaTime);
                            }
                            else
                            {
                                transform.position = Vector3.MoveTowards(transform.position, fowardPos, Speed * Time.fixedDeltaTime);
                            }
                        }
                        return true;
                    }

                    transform.position = destination.Position;
                    return false;
                });

                CurrentTile = destination;
            }
            else
            {
                await new WaitForFixedUpdate();
            }
        }

        #endregion
    }
}
