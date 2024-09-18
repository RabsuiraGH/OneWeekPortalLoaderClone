using UnityEngine;
using UnityEngine.UI;

namespace Core.StartMenu
{
    public class StartMenuController : MonoBehaviour
    {
        [SerializeField] private Button _startButton = null;
        [SerializeField] private Button _exitButton = null;

        [SerializeField] private Button _exitLevelSelectionButton = null;

        [SerializeField] private RectTransform _startMenu = null;
        [SerializeField] private RectTransform _levelSelectionMenu = null;

        [SerializeField] private LevelSelectionController _levelSelectionController;

        private void Awake()
        {
            _startButton.onClick.AddListener(OpenLevelSelection);
            _exitButton.onClick.AddListener(ExitGame);
            _exitLevelSelectionButton.onClick.AddListener(OpenStartMenu);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
            _exitLevelSelectionButton.onClick.RemoveAllListeners();
        }

        private void OpenLevelSelection()
        {
            _startMenu.gameObject.SetActive(false);
            _levelSelectionMenu.gameObject.SetActive(true);
            _levelSelectionController.PrepareUI();
        }

        private void OpenStartMenu()
        {
            _startMenu.gameObject.SetActive(true);
            _levelSelectionMenu.gameObject.SetActive(false);
        }

        public void ExitGame()
        {
#if UNITY_STANDALONE
            Application.Quit();
#endif
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}