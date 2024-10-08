using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.MainMenu.UI
{
    public class StartMenuPageUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _page = null;

        [SerializeField] private Button _startButton = null;
        [SerializeField] private Button _exitButton = null;

        public event Action OnStartButtonClicked = null;

        public event Action OnExitButtonClicked = null;

        private void Awake()
        {
            _startButton.onClick.AddListener(() => OnStartButtonClicked?.Invoke());
            _exitButton.onClick.AddListener(() => OnExitButtonClicked?.Invoke());
        }

        public void StartSelection()
        {
            _startButton.Select();
        }

        public bool IsOpen()
        {
            return _page.gameObject.activeSelf;
        }

        public void Show()
        {
            _page.gameObject.SetActive(true);
            StartSelection();
        }

        public void Hide()
        {
            _page.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _startButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }
    }
}