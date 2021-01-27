using System;
using System.Threading.Tasks;
using UnityEngine;

namespace CarGo.Game
{
    public interface IObjective
    {
        event Action OnComplete;
        bool Completed { get; }

        Task Objective();
        
        void Reset();
    }

    public interface ITile
    {
        Vector3 Position { get; }
    }
}
