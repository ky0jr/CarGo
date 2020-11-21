using System;
using Title.Path;
using Title.UI.Button;
using UnityEngine;

namespace Title.Menu.Controller
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

            playButton.ButtonDown += () => OnClick(MenuPath.Play);
            optionButton.ButtonDown += () => OnClick(MenuPath.Option);
            exitButton.ButtonDown += () => OnClick(MenuPath.Exit);

            isInitialize = true;
        }

        private void OnClick(string path)
        {
            OnClickEvent?.Invoke(path);
        }
    }
}