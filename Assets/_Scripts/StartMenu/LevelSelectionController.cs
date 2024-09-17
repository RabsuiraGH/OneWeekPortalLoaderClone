using System.Collections.Generic;
using Core.Level;
using Core.StartMenu;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core
{
    public class LevelSelectionController : MonoBehaviour
    {
        [SerializeField] private RectTransform _levelsPanelUI;
        [SerializeField] private List<Button> _levelButtons;

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
            foreach (var level in _levelDataSO.GetLevelList())
            {
                var button = Instantiate(_levelButtonPrefab, _levelsPanelUI);
                _levelButtons.Add(button);
            }
        }
    }
}