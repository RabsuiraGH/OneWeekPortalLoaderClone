using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.IngameMenu.UI
{
    public class IngameManuPageUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _page;

        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _backToStartMenuButton;

        public event Action OnContinueButtonClicked;

        public event Action OnBackToStartMenuClicked;

        private void Awake()
        {
            _continueButton.onClick.AddListener(() => OnContinueButtonClicked?.Invoke());
            _backToStartMenuButton.onClick.AddListener(() => OnBackToStartMenuClicked?.Invoke());

            if (IsOpen())
                HideMenu();
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
            _continueButton.onClick.RemoveAllListeners();
            _backToStartMenuButton.onClick.RemoveAllListeners();
        }
    }
}