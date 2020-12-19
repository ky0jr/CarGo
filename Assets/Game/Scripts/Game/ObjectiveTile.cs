using Title.Game;
using UnityEngine;

public class ObjectiveTile : Tile, IObjective
{
    public event System.Action OnComplete;
    
    public bool Completed { get; private set; }

    [SerializeField] 
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshRenderer.material.color = Color.yellow;
    }

    public void Objective()
    {
        Completed = !Completed;
        OnComplete?.Invoke();
        
        meshRenderer.material.color = Completed ? Color.red : Color.yellow;
    }
}
