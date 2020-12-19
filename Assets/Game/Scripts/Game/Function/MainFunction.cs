using System.Collections.Generic;
using Title.Game.Command;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainFunction : MonoBehaviour, IFunction
{
    public event System.Action<IFunction> OnSelected; 
    [SerializeField]
    private CommandButton prefab;
    
    public FunctionType FunctionType => FunctionType.Main;
    
    public List<CommandButton> CommandButtons { get; } = new List<CommandButton>();
    
    void IFunction.Initialize()
    {
        
    }
    
    void IFunction.AddCommand(Command command)
    {
        CommandButton button = Instantiate(prefab, transform);
        button.Initialize(command.CommandType);
        CommandButtons.Add(button);
        button.ButtonDown += () => RemoveCommand(button);
    }

    private void RemoveCommand(CommandButton button)
    {
        CommandButtons.Remove(button);
        Destroy(button.gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnSelected?.Invoke(this);
    }
}
