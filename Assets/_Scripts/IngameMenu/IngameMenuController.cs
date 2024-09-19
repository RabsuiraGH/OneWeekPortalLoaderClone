using Core.EventSystem;
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
        [SerializeField] private SceneField _startMenuScene = null;

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
            _eventBus.Subscribe<EscapeCommandUISignal>(ToggleMenu);
        }

        private void ContinueGame()
        {
            _ingameMenuPage.HideMenu();
        }

        private void BackToStartMenu()
        {
            SceneManager.LoadScene(_startMenuScene);
        }

        private void ToggleMenu(EscapeCommandUISignal signal)
        {
            if (_ingameMenuPage.IsOpen())
                _ingameMenuPage.HideMenu();
            else
                _ingameMenuPage.OpenMenu();
        }

        private void OnDestroy()
        {
            _ingameMenuPage.OnContinueButtonClicked -= ContinueGame;
            _ingameMenuPage.OnBackToStartMenuClicked -= BackToStartMenu;
        }
    }
}