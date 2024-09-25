using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.LevelStateMenu.UI
{
    public class LevelStateFailurePageUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _page;

        [SerializeField] private Button _restartLevelButton;
        [SerializeField] private Button _backToStartMenuButton;

        public event Action OnRestartLevelButtonClicked;

        public event Action OnBackToStartMenuClicked;

        private void Awake()
        {
            _restartLevelButton.onClick.AddListener(() => OnRestartLevelButtonClicked?.Invoke());
            _backToStartMenuButton.onClick.AddListener(() => OnBackToStartMenuClicked?.Invoke());
        }

        public bool IsOpen()
        {
            return _page.gameObject.activeSelf;
        }

        public void OpenMenu()
        {
            _page.gameObject.SetActive(true);
        }

        public void HideMenu()
        {
            _page.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _restartLevelButton.onClick.RemoveAllListeners();
            _backToStartMenuButton.onClick.RemoveAllListeners();
        }
    }
}