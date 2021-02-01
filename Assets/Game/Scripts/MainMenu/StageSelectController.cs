using System.Collections.Generic;
using CarGo.UI.Button;
using UnityEngine;

namespace CarGo.Menu
{
    public class StageSelectController : MonoBehaviour
    {
        public event System.Action<Path.ScenePath> OnPlay;
        private Canvas canvas;

        [SerializeField]
        private List<Button> StageButton;

        private Path.ScenePath? selectedStage = null;

        [SerializeField]
        private Button backButton;

        [SerializeField] 
        private Button playButton;

        private bool isInitialize;
        public void Initialize(int stageData)
        {
            if(isInitialize)
                return;
            
            canvas = GetComponent<Canvas>();
            for (int i = 0; i < 5 ; i++)
            {
                int index = i;

                bool checkSave = ((int)Mathf.Pow(2, i) & stageData) != 0;

                if (checkSave)
                {
                    StageButton[i].ButtonDown += () =>
                    {
                        SetButtonStage(StageButton[index], (Path.ScenePath) index + 1);
                    };
                }
                else
                {
                    StageButton[i].gameObject.SetActive(false);   
                }
            }

            backButton.ButtonDown += () => { canvas.enabled = false; };
            playButton.ButtonDown += Play;

            isInitialize = true;
        }

        private void SetButtonStage(Button button, Path.ScenePath path)
        {
            if (selectedStage.HasValue)
            {
                StageButton[(int)selectedStage - 1]._Image.color = Color.white;
            }

            selectedStage = path;
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
            if (selectedStage.HasValue)
            {
                OnPlay?.Invoke(selectedStage.Value);
            }
        }

        private void Reset()
        {
            playButton._Image.color = new Color(.5f, .5f, .5f);

            selectedStage = null;
        }
    }
}
