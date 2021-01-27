using System.Collections.Generic;
using CarGo.UI.Button;
using UnityEngine;

namespace CarGo.Menu
{
    public class StageSelectionController : MonoBehaviour
    {
        public event System.Action<string> OnPlay;
        private Canvas canvas;

        [SerializeField]
        private List<Button> StageButton;

        private Path.ScenePath? selectedPath = null;

        [SerializeField]
        private Button backButton;

        [SerializeField] 
        private Button playButton;
        private void Start()
        {
            canvas = GetComponent<Canvas>();
            for (int i = 0; i < 5 ; i++)
            {
                int index = i;
                
                StageButton[i].ButtonDown += () =>
                {
                    SetButtonStage(StageButton[index], (Path.ScenePath) index + 1);
                };
            }

            backButton.ButtonDown += () => { canvas.enabled = false; };
            playButton.ButtonDown += Play;
        }

        private void SetButtonStage(Button button, Path.ScenePath path)
        {
            if (selectedPath.HasValue)
            {
                StageButton[(int)selectedPath - 1]._Image.color = Color.white;
            }

            selectedPath = path;
            button._Image.color = new Color(.5f, .5f, .5f);
            playButton._Image.color = Color.white;
        }

        public void Show()
        {
            canvas.enabled = true;
            Reset();
        }

        private void Play()
        {
            if (selectedPath.HasValue)
            {
                OnPlay?.Invoke(selectedPath.Value.ToString());
            }
        }

        private void Reset()
        {
            playButton._Image.color = new Color(.5f, .5f, .5f);

            selectedPath = null;
        }
    }
}
