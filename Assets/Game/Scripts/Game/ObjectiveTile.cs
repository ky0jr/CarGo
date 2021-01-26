using UnityEngine;

namespace CarGo.Game
{
    public class ObjectiveTile : Tile, IObjective
    {
        public event System.Action OnComplete;

        public bool Completed { get; private set; }

        [SerializeField] private Renderer renderer;

        [SerializeField] private Material[] lightUpMaterial;

        [SerializeField] private Material[] lightOffMaterial;

        private void Start()
        {
            renderer.sharedMaterials = lightOffMaterial;
        }

        public void Objective()
        {
            Completed = !Completed;
            OnComplete?.Invoke();

            renderer.sharedMaterials = Completed ? lightUpMaterial : lightOffMaterial;
        }
    }
}
