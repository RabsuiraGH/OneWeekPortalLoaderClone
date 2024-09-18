using Core.EventSystem;
using SceneFieldTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Core.IngameMenu
{
    public class IngameMenuController : MonoBehaviour
    {
        [SerializeField] private SceneField _startMenuScene;

        [SerializeField] private RectTransform _settingsMenu;

        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _backToStartMenuButton;

        [SerializeField] private EventBus _eventBus;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _continueButton.onClick.AddListener(HideMenu);
            _backToStartMenuButton.onClick.AddListener(BackToStartMenu);
            _eventBus.Subscribe<EscapeCommandUISignal>(ToggleMenu);

            if (_settingsMenu.gameObject.activeSelf)
                HideMenu();
        }

        private void ToggleMenu(EscapeCommandUISignal signal)
        {
            if (_settingsMenu.gameObject.activeSelf)
                HideMenu();
            else
                OpenMenu();
        }

        private void OpenMenu()
        {
            _settingsMenu.gameObject.SetActive(true);
        }

        private void BackToStartMenu()
        {
            SceneManager.LoadScene(_startMenuScene);
        }

        private void HideMenu()
        {
            _settingsMenu.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _continueButton.onClick.RemoveAllListeners();
            _backToStartMenuButton.onClick.RemoveAllListeners();
        }
    }
}