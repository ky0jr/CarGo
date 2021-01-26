using System;
using CarGo.UI.Button;
using UnityEngine;

namespace CarGo.Menu.Controller
{
    public class MainMenuController : MonoBehaviour
    {
        public event Action<string> OnClickEvent;

        [SerializeField] private Button playButton;
        [SerializeField] private Button optionButton;
        [SerializeField] private Button exitButton;

        private bool isInitialize = false;

        public void Initialize()
        {
            if (isInitialize)
                return;

            playButton.ButtonDown += () => OnClick(Path.MenuPath.Play);
            optionButton.ButtonDown += () => OnClick(Path.MenuPath.Option);
            exitButton.ButtonDown += () => OnClick(Path.MenuPath.Exit);

            isInitialize = true;
        }

        private void OnClick(string path)
        {
            OnClickEvent?.Invoke(path);
        }
    }
}