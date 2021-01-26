using UnityEngine;

namespace CarGo.Game.Function
{
    public class CommandButton : UI.Button.Button
    {
        public Command Command { get; private set; }

        private bool isInitialize = false;

        public void Initialize(CommandType commandType)
        {
            if (isInitialize)
                return;

            Command = new Command(commandType);
            string path = $"Game/CommandButton/command_{commandType.ToString().ToLower()}";
            //Debug.Log(path);
            Sprite sprite = Resources.Load<Sprite>(path);
            _Image.sprite = sprite;
            isInitialize = true;
        }
    }
}
