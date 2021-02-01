using CarGo.Data;
using CarGo.Menu.Controller;
using CarGo.Scene;
using UnityEngine;
using UnityEngine.Serialization;

namespace CarGo.Menu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private MainMenuController mainMenuController;

        [SerializeField] private SceneManager sceneManager;

        [FormerlySerializedAs("stageSelectionController")] [SerializeField] private StageSelectController stageSelectController;

        private void Start()
        {
            mainMenuController.OnClickEvent += OnMenuChange;
            mainMenuController.Initialize();
            stageSelectController.Initialize(DataManager.LoadData());
            stageSelectController.OnPlay += sceneManager.ChangeScene;
        }

        private void OnMenuChange(string path)
        {
            switch (path)
            {
                case Path.MenuPath.Play:
                    stageSelectController.Show();
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
