using System.Collections.Generic;
using CarGo.Game.Controller;
using CarGo.Game.Function;
using UnityEngine;
using UnityEngine.Serialization;

namespace CarGo.Game
{
    public class FunctionManager : MonoBehaviour
    {
        [SerializeField] private CommandController _commandController;

        [SerializeField] private FunctionController _functionController;

        [FormerlySerializedAs("_commandTypes")] [SerializeField]
        private List<CommandType> _listOfCommand;

        [SerializeField] private CanvasGroup canvasGroup;

        private bool isInitialize = false;

        public void Initialize()
        {
            if (isInitialize)
                return;

            _functionController.Initialize();
            _commandController.Initialize(_listOfCommand);
            _commandController.AddCommandEvent += _functionController.AddCommand;
            isInitialize = true;

            canvasGroup.blocksRaycasts = true;
        }

        public IEnumerable<Function.Command> CommandList(FunctionType functionType)
        {
            return _functionController.CommandList(functionType);
        }

        public void ActiveRaycast(bool value)
        {
            canvasGroup.blocksRaycasts = value;
        }

        public void ResetFunction()
        {
            _functionController.ResetFunction();
        }
    }
}
