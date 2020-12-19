using Title.Game.Command;
using UnityEngine;
using UnityEngine.UI;
using Button = Title.UI.Button.Button;

public class CommandButton : Button
{
    public Command Command { get; private set; }

    private Image _image;

    private bool isInitialize = false;
    
    public void Initialize(CommandType commandType)
    {
        if(isInitialize)
            return;
        
        Command = new Command(commandType);
        string path = $"Game/CommandButton/command_{commandType.ToString().ToLower()}";
        Debug.Log(path);
        Sprite sprite = Resources.Load<Sprite>(path);
        _image = GetComponent<Image>();
        _image.sprite = sprite;
        isInitialize = true;
    }
}
