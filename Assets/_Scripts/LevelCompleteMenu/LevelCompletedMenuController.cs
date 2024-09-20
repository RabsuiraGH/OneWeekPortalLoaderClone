using Core.EventSystem;
using Core.EventSystem.Signals;
using Core.LevelCompletedMenu.UI;
using SceneFieldTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.LevelCompletedMenu.Controller
{
    public class LevelCompletedMenuController : MonoBehaviour
    {
        [SerializeField] private LevelCompletedMenuPageUI _levelCompletedMenuPage = null;
        [SerializeField] private SceneField _mainMenuScene = null;

        [SerializeField] private EventBus _eventBus = null;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _levelCompletedMenuPage.OnNextLevelButtonClicked += StartNextLevel;
            _levelCompletedMenuPage.OnBackToStartMenuClicked += BackToStartMenu;
        }

        private void StartNextLevel()
        {
            _eventBus.Invoke(new CloseCompletelyUISignal());
            _levelCompletedMenuPage.HideMenu();

            //TODO: Start next level logic
        }

        private void BackToStartMenu()
        {
            _eventBus.Invoke(new OpenCompletelyUISignal());
            SceneManager.LoadScene(_mainMenuScene);
        }

        private void OnDestroy()
        {
            _levelCompletedMenuPage.OnNextLevelButtonClicked -= StartNextLevel;
            _levelCompletedMenuPage.OnBackToStartMenuClicked -= BackToStartMenu;
        }
    }
}