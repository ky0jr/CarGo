using Title.Menu.Controller;
using Title.Scene;
using UnityEngine;

namespace Title.Menu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private MainMenuController mainMenuController;

        [SerializeField] private SceneManager sceneManager;

        private void Start()
        {
            mainMenuController.OnClickEvent += OnMenuChange;
            mainMenuController.Initialize();
        }

        private void OnMenuChange(string path)
        {
            switch (path)
            {
                case Path.MenuPath.Play:
                    sceneManager.ChangeScene(Path.ScenePath.Stage1);
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
