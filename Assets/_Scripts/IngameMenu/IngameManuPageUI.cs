using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.IngameMenu.UI
{
    public class IngameManuPageUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _page;

        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _backToStartMenuButton;

        public event Action OnContinueButtonClicked;

        public event Action OnRestartButtonClicked;

        public event Action OnBackToStartMenuClicked;

        private void Awake()
        {
            _continueButton.onClick.AddListener(() => OnContinueButtonClicked?.Invoke());
            _restartButton.onClick.AddListener(() => OnRestartButtonClicked?.Invoke());
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
            _continueButton.Select();
        }

        private void OnDestroy()
        {
            _continueButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();
            _backToStartMenuButton.onClick.RemoveAllListeners();
        }
    }
}