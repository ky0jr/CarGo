using System.Collections.Generic;
using Title.Game.Command;
using UnityEngine;

public class FunctionController : MonoBehaviour
{
    private IFunction currentFunction;

    private IFunction[] _functions;
    
    public void Initialize()
    {
        _functions = GetComponentsInChildren<IFunction>();
        foreach (var function in _functions)
        {
            function.OnSelected += SelectFunction;
            
            if (function.FunctionType == FunctionType.Main)
                currentFunction = function;
        }
    }
    public void AddCommand(Command command)
    {
        currentFunction.AddCommand(command);
    }

    private void SelectFunction(IFunction function)
    {
        currentFunction = function;
    }

    public IEnumerable<Command> Commands(FunctionType functionType)
    {
        List<Command> commands = new List<Command>();

        foreach (IFunction function in _functions)
        {
            if (function.FunctionType == functionType)
            {
                foreach (var button in function.CommandButtons)
                {
                    commands.Add(button.Command);
                }
            }
        }

        return commands;
    }
}
