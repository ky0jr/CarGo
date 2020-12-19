using System.Collections.Generic;
using Title.Game.Command;
using UnityEngine.EventSystems;

public interface IFunction: IPointerDownHandler
{
    event System.Action<IFunction> OnSelected;
    FunctionType FunctionType { get; }
    
    List<CommandButton> CommandButtons { get; }

    void Initialize();
    
    void AddCommand(Command command);
}
