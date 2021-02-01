using System.Threading.Tasks;
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

        private AudioSource _audioSource;
        private void Start()
        {
            Reset();
            _audioSource = GetComponent<AudioSource>();
        }

        async Task IObjective.Objective()
        {
            Completed = !Completed;
            renderer.sharedMaterials = Completed ? lightUpMaterial : lightOffMaterial;
            _audioSource.Play();
            await new WaitForSeconds(.25f);
            OnComplete?.Invoke();
        }

        public void Reset()
        {
            Completed = false;
            renderer.sharedMaterials = lightOffMaterial;
        }
    }
}
