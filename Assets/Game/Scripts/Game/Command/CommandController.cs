using System;
using System.Collections.Generic;
using CarGo.Game.Function;
using UnityEngine;

namespace CarGo.Game.Controller
{
    public class CommandController : MonoBehaviour
    {
        public event Action<Function.Command> AddCommandEvent;
        [SerializeField] private CommandButton commandButtonPrefab;

        [SerializeField] private Transform commandBox;

        private bool isInitialize = false;

        public void Initialize(IEnumerable<CommandType> commandTypes)
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

        private void AddCommand(Function.Command command)
        {
            AddCommandEvent?.Invoke(command);
        }
    }
}
