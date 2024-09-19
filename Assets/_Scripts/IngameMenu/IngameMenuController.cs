using Core.EventSystem;
using Core.EventSystem.Signals;
using Core.IngameMenu.UI;
using SceneFieldTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.IngameMenu
{
    public class IngameMenuController : MonoBehaviour
    {
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
            _ingameMenuPage.OnContinueButtonClicked += ContinueGame;
            _ingameMenuPage.OnBackToStartMenuClicked += BackToStartMenu;
            _eventBus.Subscribe<EscapeCommandSignal>(ToggleMenu);
        }

        private void ContinueGame()
        {
            _eventBus.Invoke(new CloseCompletelyUISignal());
            _ingameMenuPage.HideMenu();
        }

        private void BackToStartMenu()
        {
            _eventBus.Invoke(new OpenCompletelyUISignal());
            SceneManager.LoadScene(_mainMenuScene);
        }

        private void ToggleMenu(EscapeCommandSignal signal)
        {
            if (_ingameMenuPage.IsOpen())
            {
                _ingameMenuPage.HideMenu();
                _eventBus.Invoke(new CloseCompletelyUISignal());
            }
            else
            {
                _ingameMenuPage.OpenMenu();
                _eventBus.Invoke(new OpenCompletelyUISignal());
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