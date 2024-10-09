using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class GameRuleTipsPageUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _controlPanelUI = null;
        [SerializeField] private RectTransform _goalPanelUI = null;
        [SerializeField] private RectTransform _chargePanelUI = null;

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
            _chargeCloseButton.onClick.AddListener(()=> OnChargePageClosedButtonPressed?.Invoke());
        }

        public void ToggleControlPanel(bool show)
        {
            _controlPanelUI.gameObject.SetActive(show);
            _controlCloseButton.Select();
        }

        public void ToggleGoalPanel(bool show)
        {
            _goalPanelUI.gameObject.SetActive(show);
            _goalCloseButton.Select();
        }

        public void ToggleChargePanel(bool show)
        {
            _chargePanelUI.gameObject.SetActive(show);
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