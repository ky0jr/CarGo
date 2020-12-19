using System.Collections.Generic;
using Title.Game.Command;
using UnityEngine;

public class FunctionManager : MonoBehaviour
{
    [SerializeField]
    private CommandController _commandController;

    [SerializeField] 
    private FunctionController _functionController;

    [SerializeField] 
    private CommandType[] _commandTypes;
    
    private bool isInitialize = false;

    public void Initialize()
    {
        if(isInitialize)
            return;
        
        _functionController.Initialize();
        _commandController.Initialize(_commandTypes);
        _commandController.AddCommandEvent += _functionController.AddCommand;
        isInitialize = true;
    }

    public IEnumerable<Command> CommandList(FunctionType functionType)
    {
        return _functionController.Commands(functionType);
    }
}
