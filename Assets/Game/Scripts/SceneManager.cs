using System.Threading.Tasks;
using UnityEngine;

namespace CarGo.Scene
{
    public class SceneManager : MonoBehaviour
    {
        private static readonly int FadeOut = Animator.StringToHash("FadeOut");
        private static readonly int FadeIn = Animator.StringToHash("FadeIn");
        
        private Animator animator;
        private Canvas canvas;
        private async void Awake()
        {
            animator = GetComponent<Animator>();
            canvas = GetComponent<Canvas>();

            canvas.enabled = true;
            
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
