using Title.Game.Command;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    public event System.Action<Command> AddCommandEvent;
    [SerializeField]
    private CommandButton commandButtonPrefab;

    [SerializeField] 
    private Transform commandBox;

    private bool isInitialize = false;
    
    public void Initialize(CommandType[] commandTypes)
    {
        if (isInitialize)
            return;
        
        foreach (CommandType commandType in commandTypes)
        {
            CommandButton button = Instantiate(commandButtonPrefab, commandBox);
            button.Initialize(commandType);
            button.ButtonDown += () => AddCommand(button.Command);
        }

        isInitialize = true;
    }

    private void AddCommand(Command command)
    {
        AddCommandEvent?.Invoke(command);
    }
}
