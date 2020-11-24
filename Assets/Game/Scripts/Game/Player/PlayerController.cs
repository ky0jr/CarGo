using System.Threading.Tasks;
using Title.Game.Command;
using UnityEngine;

namespace Title.Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        public Vector3 MoveDirection { private get; set; } = Vector3.right;

        private float _speed = 2f;

        private float _rotationSpeed = 120f;
        
        public Task CurrentTask { private set; get; } = Task.CompletedTask;
        
        private async Task Move()
        {
            Vector3? destination = GameManager.Instance.NextTile(transform.position, MoveDirection);
            
            if (destination.HasValue)
            {
                await new WaitWhile(() =>
                {
                    if (Vector3.Distance(transform.position, destination.Value) > 0.1f)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, destination.Value, _speed * Time.fixedDeltaTime);
                        return true;
                    }

                    transform.position = destination.Value;
                    return false;
                });
            }
            else
            {
                await new WaitForFixedUpdate();
            }
        }

        private void Update()
        {
            if (Input.GetButtonDown("Jump"))
            { 
                RunCommand(CommandType.Move);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                RunCommand(CommandType.RotateLeft);
            }
            
            if (Input.GetKeyDown(KeyCode.D))
            {
                RunCommand(CommandType.RotateRight);
            }
        }

        public async void RunCommand(CommandType commandType)
        {
            if(!CurrentTask.IsCompleted)
                return;
            
            switch (commandType)
            {
                case CommandType.Move:
                    CurrentTask = Move();
                    break;
                case CommandType.RotateLeft:
                case CommandType.RotateRight:
                    CurrentTask = Rotate(commandType);
                    break;
            }

            await CurrentTask;
        }

        private async Task Rotate(CommandType commandType)
        {
            Vector3 direction = commandType == CommandType.RotateLeft ? Vector3.down : Vector3.up;

            var rotation = (transform.rotation * Quaternion.Euler(0, 90 * direction.y, 0));

            await new WaitWhile(() =>
            {
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
    }
}
