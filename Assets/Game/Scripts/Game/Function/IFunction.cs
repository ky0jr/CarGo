using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace CarGo.Game.Function
{
    public interface IFunction : IPointerDownHandler
    {
        event Action<IFunction> OnSelected;
        FunctionType FunctionType { get; }

        List<CommandButton> ListOfCommandButton { get; }

        void AddCommand(Command command);

        void Clear();

        void Deselect();
    }
}
