using System.Collections.Generic;
using System.Linq;
using Core.Level;
using Core.StartMenu;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Core
{
    public class LevelSelectionController : MonoBehaviour
    {
        [SerializeField] private RectTransform _levelsPanelUI;
        [SerializeField] private List<LevelButtonUI> _levelButtons;

        [SerializeField] private LevelButtonUI _levelButtonPrefab;

        [SerializeField] private LevelsDataSO _levelDataSO;

        [Inject]
        public void Construct(LevelsDataSO levelsData, LevelButtonUI levelButtonUI)
        {
            _levelDataSO = levelsData;
            _levelButtonPrefab = levelButtonUI;
        }

        [ContextMenu(nameof(PrepareUI))]
        public void PrepareUI()
        {
            _levelButtons.Clear();

            for (int i = 0; i < _levelsPanelUI.childCount; i++)
            {
                DestroyImmediate(_levelsPanelUI.GetChild(i).gameObject);
            }

            foreach (LevelData level in _levelDataSO.GetLevelList())
            {
                LevelButtonUI button = Instantiate(_levelButtonPrefab, _levelsPanelUI);
                button.PrepareButton(level.LevelName);
                _levelButtons.Add(button);
                button.OnClick += StartLevel;
            }
        }

        private void StartLevel(LevelButtonUI button)
        {
            int index = _levelButtons.IndexOf(button);
            LevelData l = _levelDataSO.GetLevelAt(index);
            SceneManager.LoadScene(l.LevelScene);
        }
    }
}