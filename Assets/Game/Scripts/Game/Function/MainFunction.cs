using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CarGo.Game.Function
{
    public class MainFunction : MonoBehaviour, IFunction
    {
        private const int MaxCommand = 16;
        public event Action<IFunction> OnSelected;
        
        [SerializeField] private CommandButton prefab;

        public FunctionType FunctionType => FunctionType.Main;

        public List<CommandButton> ListOfCommandButton { get; } = new List<CommandButton>();

        void IFunction.Initialize()
        {

        }

        void IFunction.AddCommand(Function.Command command)
        {
            if (ListOfCommandButton.Count >= 15)
                return;

            CommandButton button = Instantiate(prefab, transform);
            button.Initialize(command.CommandType);
            ListOfCommandButton.Add(button);
            button.ButtonDown += () => RemoveCommand(button);
        }

        void IFunction.Clear()
        {
            foreach (CommandButton button in ListOfCommandButton)
            {
                Destroy(button.gameObject);
            }

            ListOfCommandButton.Clear();
        }

        private void RemoveCommand(CommandButton button)
        {
            ListOfCommandButton.Remove(button);
            Destroy(button.gameObject);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnSelected?.Invoke(this);
        }
    }
}
