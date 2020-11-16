using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Scene
{
    public class SceneManager : MonoBehaviour
    {
        private static readonly int FadeOut = Animator.StringToHash("FadeOut");
        private static readonly int FadeIn = Animator.StringToHash("FadeIn");
        
        private Animator animator;

        private async void Awake()
        {
            animator = GetComponent<Animator>();

            await Crossfade(FadeIn);
        }

        public async void ChangeScene(string scene)
        {
            await Crossfade(FadeOut);

            UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
        }

        private async Task Crossfade(int animation)
        {
            Time.timeScale = 1;
            animator.SetTrigger(animation);

            await new WaitForSeconds(1);
        }
    }
}
