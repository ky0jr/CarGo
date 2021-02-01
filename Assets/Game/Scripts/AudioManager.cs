using UnityEngine;

namespace CarGo.Audio
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;
        private void Awake()
        {
            if (_instance is null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}