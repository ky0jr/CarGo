using System.Collections.Generic;
using CarGo.Game.Function;
using UnityEngine;

namespace CarGo.Game.Controller
{
    public class FunctionController : MonoBehaviour
    {
        private IFunction currentFunction;

        private List<IFunction> _functions;

        private bool isInitialize;

        public void Initialize()
        {
            if (isInitialize)
            {
                return;
                ;
            }

            _functions = new List<IFunction>(GetComponentsInChildren<IFunction>());
            foreach (var function in _functions)
            {
                function.OnSelected += SelectFunction;

                if (function.FunctionType == FunctionType.Main)
                    currentFunction = function;
                else
                    function.Deselect();
            }

            isInitialize = true;
        }

        public void AddCommand(Command command)
        {
            currentFunction.AddCommand(command);
        }

        private void SelectFunction(IFunction function)
        {
            currentFunction.Deselect();
            currentFunction = function;
        }

        public IEnumerable<Command> CommandList(FunctionType functionType)
        {
            List<Command> commands = new List<Command>();

            foreach (IFunction function in _functions)
            {
                if (function.FunctionType == functionType)
                {
                    foreach (var button in function.ListOfCommandButton)
                    {
                        commands.Add(button.Command);
                    }
                }
            }

            return commands;
        }

        public void ResetFunction()
        {
            foreach (IFunction function in _functions)
            {
                function.Clear();
            }
        }
    }
}
