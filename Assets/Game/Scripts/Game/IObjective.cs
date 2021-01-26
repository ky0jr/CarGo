using System;
using UnityEngine;

namespace CarGo.Game
{
    public interface IObjective
    {
        event Action OnComplete;
        bool Completed { get; }

        void Objective();
    }

    public interface ITile
    {
        Vector3 Position { get; }
    }
}
