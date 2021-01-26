using System.Collections.Generic;
using CarGo.Game.Function;
using UnityEngine;

namespace CarGo.Game.Controller
{
    public class FunctionController : MonoBehaviour
    {
        private IFunction currentFunction;

        private List<IFunction> _functions;

        private bool isInitialize = false;

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
            }

            isInitialize = true;
        }

        public void AddCommand(Function.Command command)
        {
            currentFunction.AddCommand(command);
        }

        private void SelectFunction(IFunction function)
        {
            currentFunction = function;
        }

        public IEnumerable<Function.Command> CommandList(FunctionType functionType)
        {
            List<Function.Command> commands = new List<Function.Command>();

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
