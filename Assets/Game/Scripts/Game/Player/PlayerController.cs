using System.Threading;
using System.Threading.Tasks;
using Title.Game.Command;
using UnityEngine;

namespace Title.Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        public Vector3 MoveDirection { private get; set; }

        public ITile CurrentTile { get; set; }

        private float _speed = 1.5f;

        private float _rotationSpeed = 270f;
        
        public Task CurrentTask { private set; get; } = Task.CompletedTask;

        public async Task RunCommand(CommandType commandType, CancellationToken cancellationToken)
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
                case CommandType.Action:
                    CurrentTask = Action(cancellationToken);
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
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
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
                        transform.position = Vector3.MoveTowards(transform.position, destination.Position, _speed * Time.fixedDeltaTime);
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

        private async Task Action(CancellationToken cancellationToken)
        {
            if (CurrentTile is IObjective objective)
            {
                objective.Objective();
            }

            await new WaitForFixedUpdate();
        }

        #endregion
    }
}
