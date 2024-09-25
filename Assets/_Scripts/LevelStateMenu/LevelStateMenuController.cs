using Core.EventSystem;
using Core.EventSystem.Signals;
using Core.LevelStateMenu.UI;
using SceneFieldTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.LevelStateMenu.Controller
{
    public class LevelStateMenuController : MonoBehaviour
    {
        [SerializeField] private LevelStateCompletedPageUI _levelCompletedPage = null;
        [SerializeField] private LevelStateFailurePageUI _levelFailurePage = null;

        [SerializeField] private Camera _menuCamera = null;

        [SerializeField] private SceneField _mainMenuScene = null;

        [SerializeField] private EventBus _eventBus = null;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _eventBus.Subscribe<LevelCompletedSignal>(OpenCompletedPage);
            _eventBus.Subscribe<LevelFailureSignal>(OpenFailurePage);

            _levelCompletedPage.OnNextLevelButtonClicked += StartNextLevel;
            _levelCompletedPage.OnBackToStartMenuClicked += BackToStartMenu;

            _levelFailurePage.OnRestartLevelButtonClicked += ResetCurrentLevel;
            _levelFailurePage.OnBackToStartMenuClicked += BackToStartMenu;
        }

        private void Start()
        {
            if (_levelCompletedPage.IsOpen())
                _levelCompletedPage.HideMenu();

            if (_levelFailurePage.IsOpen())
                _levelFailurePage.HideMenu();

            if (_menuCamera.gameObject.activeSelf)
                _menuCamera.gameObject.SetActive(false);
        }

        private void ResetCurrentLevel()
        {
            _eventBus.Invoke(new CloseCompletelyUISignal());
            _levelFailurePage?.HideMenu();

            _eventBus.Invoke(new LevelResetSignal());
        }

        private void StartNextLevel()
        {
            _eventBus.Invoke(new CloseCompletelyUISignal());
            _levelCompletedPage.HideMenu();

            _eventBus.Invoke(new LevelLoadNextSignal());
        }

        private void OpenCompletedPage(LevelCompletedSignal signal)
        {
            _levelCompletedPage.OpenMenu();
            ToggleCamera(true);
            _eventBus.Invoke(new OpenCompletelyUISignal());
        }

        private void OpenFailurePage(LevelFailureSignal signal)
        {
            _levelFailurePage.OpenMenu();
            ToggleCamera(true);
            _eventBus.Invoke(new OpenCompletelyUISignal());
        }

        private void BackToStartMenu()
        {
            _eventBus.Invoke(new OpenCompletelyUISignal());
            SceneManager.LoadScene(_mainMenuScene);
        }

        private void ToggleCamera(bool active)
        {
            _menuCamera.gameObject.SetActive(active);
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<LevelCompletedSignal>(OpenCompletedPage);
            _eventBus.Unsubscribe<LevelFailureSignal>(OpenFailurePage);

            _levelCompletedPage.OnNextLevelButtonClicked -= StartNextLevel;
            _levelCompletedPage.OnBackToStartMenuClicked -= BackToStartMenu;

            _levelFailurePage.OnRestartLevelButtonClicked -= ResetCurrentLevel;
            _levelFailurePage.OnBackToStartMenuClicked -= BackToStartMenu;
        }
    }
}