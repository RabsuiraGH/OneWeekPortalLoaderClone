using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.LevelStateMenu.UI
{
    public class LevelStateCompletedPageUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _page;

        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _backToStartMenuButton;

        public event Action OnNextLevelButtonClicked;

        public event Action OnBackToStartMenuClicked;

        private void Awake()
        {
            _nextLevelButton.onClick.AddListener(() => OnNextLevelButtonClicked?.Invoke());
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
            _nextLevelButton.Select();
        }

        private void OnDestroy()
        {
            _nextLevelButton.onClick.RemoveAllListeners();
            _backToStartMenuButton.onClick.RemoveAllListeners();
        }
    }
}