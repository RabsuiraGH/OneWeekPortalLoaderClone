using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.GameplayInterface.UI
{
    public class BatteryChargePanelUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _panel;
        [SerializeField] private Vector2Int _cellSize = new Vector2Int(64, 32);

        [SerializeField] private RectOffset _padding;

        [SerializeField] private Vector2Int _spacing = new Vector2Int(8, 8);

        [SerializeField] private GameObject _batteryChargeCellPrefab;

        [SerializeField] private List<GameObject> _batteryChargeCells;

#if UNITY_EDITOR

        [EasyButtons.Button]
        private void SetDefaultPaddong()
        {
            _padding = new RectOffset(8, 8, 8, 8);
        }

#endif

        [EasyButtons.Button]
        public void PrepareUI(int maximumCellCount, int activeCellCount)
        {
            int height = (int)(maximumCellCount * (_cellSize.y + _spacing.y) - _spacing.y + _padding.top + _padding.bottom);
            int width = (int)(_cellSize.x + _padding.left + _padding.right);
            _panel.sizeDelta = new Vector2(width, height);

            _batteryChargeCells.Clear();
            for (int i = _panel.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(_panel.GetChild(i).gameObject);
            }

            for (int i = 0; i < maximumCellCount; i++)
            {
                GameObject cell = Instantiate(_batteryChargeCellPrefab, _panel);
                _batteryChargeCells.Add(cell);
                cell.name += $"_{i}";
            }
            UpdateUI(activeCellCount);
        }

        public void UpdateUI(int activeCellCount)
        {
            if (activeCellCount > _batteryChargeCells.Count)
                PrepareUI(activeCellCount, activeCellCount);

            for (int i = 0; i < _batteryChargeCells.Count; i++)
            {
                if (i < activeCellCount)
                    _batteryChargeCells[i].SetActive(true);
                else
                    _batteryChargeCells[i].SetActive(false);
            }
        }
    }
}