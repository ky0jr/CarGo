using CarGo.Menu.Controller;
using CarGo.Scene;
using UnityEngine;

namespace CarGo.Menu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private MainMenuController mainMenuController;

        [SerializeField] private SceneManager sceneManager;

        [SerializeField] private StageSelectionController stageSelectionController;

        private void Start()
        {
            mainMenuController.OnClickEvent += OnMenuChange;
            mainMenuController.Initialize();
            stageSelectionController.OnPlay += sceneManager.ChangeScene;
        }

        private void OnMenuChange(string path)
        {
            switch (path)
            {
                case Path.MenuPath.Play:
                    stageSelectionController.Show();
                    break;
                case Path.MenuPath.Option:
                    break;
                case Path.MenuPath.Exit:
                    Application.Quit();
                    break;
            }
        }
    }
}
