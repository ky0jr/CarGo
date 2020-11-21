using Title.Menu.Controller;
using Title.Path;
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
            mainMenuController.OnClickEvent += Open;
            mainMenuController.Initialize();
        }

        private void Open(string path)
        {
            if (path.Equals(MenuPath.Play))
            {
                sceneManager.ChangeScene(ScenePath.Stage1);
            }
            else if (path.Equals(MenuPath.Option))
            {

            }
            else if (path.Equals(MenuPath.Exit))
            {
                Application.Quit();
            }
        }
    }
}
