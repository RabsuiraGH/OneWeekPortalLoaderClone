using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.LevelStateMenu.UI
{
    public class LevelStateFailurePageUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _page = null;

        [SerializeField] private Button _restartLevelButton = null;
        [SerializeField] private Button _backToStartMenuButton = null;

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

            StartSelection();
        }

        public void HideMenu()
        {
            _page.gameObject.SetActive(false);
        }

        public void StartSelection()
        {
            _restartLevelButton.Select();
        }

        private void OnDestroy()
        {
            _restartLevelButton.onClick.RemoveAllListeners();
            _backToStartMenuButton.onClick.RemoveAllListeners();
        }
    }
}