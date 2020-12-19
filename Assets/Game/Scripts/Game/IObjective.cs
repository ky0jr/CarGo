using UnityEngine;

public interface IObjective
{
    event System.Action OnComplete;
    bool Completed { get; }

    void Objective();
}

public interface ITile
{
    Vector3 Position { get; }
}
