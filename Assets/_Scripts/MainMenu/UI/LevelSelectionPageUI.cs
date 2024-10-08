using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.MainMenu.UI
{
    public class LevelSelectionPageUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _page = null;
        [SerializeField] private RectTransform _levelsPage = null;

        [SerializeField] private Button _exitButton = null;

        [SerializeField] private List<LevelButtonUI> _levelButtons = new();
        [SerializeField] private LevelButtonUI _levelButtonPrefab = null;

        public event Action OnExitButtonClicked = null;

        public event Action<int> OnLevelSelected = null;

        [Inject]
        public void Construct(LevelButtonUI levelButtonPrefab)
        {
            _levelButtonPrefab = levelButtonPrefab;
        }

        private void Awake()
        {
            _exitButton.onClick.AddListener(() => OnExitButtonClicked?.Invoke());
        }

        public void PrepareUI(IEnumerable<string> levelNames)
        {
            ClearUI();
            foreach (string levelName in levelNames)
            {
                LevelButtonUI button = Instantiate(_levelButtonPrefab, _levelsPage);
                button.PrepareButton(levelName);
                _levelButtons.Add(button);
                button.OnClick += OnLevelButtonClicked;
            }
        }

        public void ClearUI()
        {
            _levelButtons.Clear();

            for (int i = 0; i < _levelsPage.childCount; i++)
            {
                GameObject item = _levelsPage.GetChild(i).gameObject;

                if (item.TryGetComponent(out LevelButtonUI button))
                {
                    button.OnClick -= OnLevelButtonClicked;
                }
                DestroyImmediate(item);
            }
        }

        private void OnLevelButtonClicked(LevelButtonUI uI)
        {
            int index = _levelButtons.IndexOf(uI);
            OnLevelSelected?.Invoke(index);
        }
        public void StartSelection()
        {
            if (_levelButtons.Count <= 0) return;

            _levelButtons[0].Select();
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
            _exitButton.onClick.RemoveAllListeners();
        }


    }
}