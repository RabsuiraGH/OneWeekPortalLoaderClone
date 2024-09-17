using UnityEngine;
using UnityEngine.UI;

namespace Core.StartMenu
{
    public class StartMenuController : MonoBehaviour
    {
        [SerializeField] private Button _startButton = null;
        [SerializeField] private Button _exitButton = null;

        [SerializeField] private RectTransform _startMenu = null;
        [SerializeField] private RectTransform _levelSelectionMenu = null;

        private void Awake()
        {
            _startButton.onClick.AddListener(OpenLevelSelection);
            _exitButton.onClick.AddListener(ExitGame);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }

        private void OpenLevelSelection()
        {
            _startMenu.gameObject.SetActive(false);
            _levelSelectionMenu.gameObject.SetActive(true);
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