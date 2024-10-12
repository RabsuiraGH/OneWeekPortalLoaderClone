using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.GameRuleTips.UI
{
    public class GameRuleTipsPageUI : MonoBehaviour
    {
        [field:SerializeField] public RectTransform ControlPanelUI { get; private set; } = null;
        [field: SerializeField] public RectTransform GoalPanelUI { get; private set; } = null;
        [field: SerializeField] public RectTransform ChargePanelUI { get; private set; } = null;

        [SerializeField] private Button _controlCloseButton = null;
        [SerializeField] private Button _goalCloseButton = null;
        [SerializeField] private Button _chargeCloseButton = null;

        public event Action OnControlPageCloseButtonPressed;

        public event Action OnGoalPageClosedButtonPressed;

        public event Action OnChargePageClosedButtonPressed;

        private void Awake()
        {
            _controlCloseButton.onClick.AddListener(() => OnControlPageCloseButtonPressed?.Invoke());
            _goalCloseButton.onClick.AddListener(() => OnGoalPageClosedButtonPressed?.Invoke());
            _chargeCloseButton.onClick.AddListener(() => OnChargePageClosedButtonPressed?.Invoke());
        }

        public void ToggleControlPanel(bool show)
        {
            ControlPanelUI.gameObject.SetActive(show);
            _controlCloseButton.Select();
        }

        public void ToggleGoalPanel(bool show)
        {
            GoalPanelUI.gameObject.SetActive(show);
            _goalCloseButton.Select();
        }

        public void ToggleChargePanel(bool show)
        {
            ChargePanelUI.gameObject.SetActive(show);
            _chargeCloseButton.Select();
        }

        private void OnDestroy()
        {
            _controlCloseButton.onClick.RemoveAllListeners();
            _goalCloseButton.onClick.RemoveAllListeners();
            _chargeCloseButton.onClick.RemoveAllListeners();
        }
    }
}