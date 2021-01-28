using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CarGo.Game.Function
{
    public abstract class Function : MonoBehaviour, IFunction
    {
        protected abstract int MaxCommand { get; }
        public event Action<IFunction> OnSelected;
        
        [SerializeField] 
        private CommandButton prefab;

        public abstract FunctionType FunctionType { get; }

        public List<CommandButton> ListOfCommandButton { get; } = new List<CommandButton>();
        
        [SerializeField]
        private Image background;

        void IFunction.AddCommand(Command command)
        {
            if (ListOfCommandButton.Count >= MaxCommand)
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
            background.color = Color.white;
        }

        void IFunction.Deselect()
        {
            background.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }
}
