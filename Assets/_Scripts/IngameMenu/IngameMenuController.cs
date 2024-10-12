using Core.GameEventSystem;
using Core.GameEventSystem.Signals;
using Core.IngameMenu.UI;
using SceneFieldTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.IngameMenu.Controller
{
    public class IngameMenuController : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas = null;
        [SerializeField] private IngameManuPageUI _ingameMenuPage = null;
        [SerializeField] private SceneField _mainMenuScene = null;

        [SerializeField] private EventBus _eventBus = null;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _canvas.worldCamera = Camera.main;

            _ingameMenuPage.OnContinueButtonClicked += ContinueGame;
            _ingameMenuPage.OnRestartButtonClicked += RestartLevel;
            _ingameMenuPage.OnBackToStartMenuClicked += BackToStartMenu;
            _eventBus.Subscribe<EscapeCommandSignal>(ToggleMenu);
        }

        private void Start()
        {
            if (_ingameMenuPage.IsOpen())
                _ingameMenuPage.HideMenu();
        }

        private void ContinueGame()
        {
            _eventBus.Invoke(new CloseUISignal(_ingameMenuPage.transform));
            _ingameMenuPage.HideMenu();
        }

        private void RestartLevel()
        {
            _eventBus.Invoke(new CloseUISignal(_ingameMenuPage.transform));
            _eventBus.Invoke(new LevelResetSignal());
        }

        private void BackToStartMenu()
        {
            _eventBus.Invoke(new OpenUISignal(null));
            SceneManager.LoadScene(_mainMenuScene);
        }

        private void ToggleMenu(EscapeCommandSignal signal)
        {
            if (_ingameMenuPage.IsOpen())
            {
                _ingameMenuPage.HideMenu();
                _eventBus.Invoke(new CloseUISignal(_ingameMenuPage.transform));
            }
            else
            {
                _ingameMenuPage.OpenMenu();
                _eventBus.Invoke(new OpenUISignal(_ingameMenuPage.transform));

            }
        }

        private void OnDestroy()
        {
            _ingameMenuPage.OnContinueButtonClicked -= ContinueGame;
            _ingameMenuPage.OnBackToStartMenuClicked -= BackToStartMenu;
            _eventBus.Unsubscribe<EscapeCommandSignal>(ToggleMenu);
        }
    }
}