using Core.GameEventSystem;
using Core.GameEventSystem.Signals;
using Core.GameplayInterface.UI;
using UnityEngine;
using Zenject;

namespace Core.GameplayInterface.Controller
{
    public class BatteryChargePanelController : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private BatteryChargePanelUI _panel;

        [SerializeField] private EventBus _eventBus;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _canvas.worldCamera = Camera.main;

            _panel.Hide();
            _panel.ClearUI();

            _eventBus.Subscribe<BatteryInfoSignal>(PrepareUI);
            _eventBus.Subscribe<BatteryChargeChangedSignal>(UpdateUI);
        }

        private void UpdateUI(BatteryChargeChangedSignal signal)
        {
            _panel.UpdateUI(signal.BatteryCharge);
        }

        private void PrepareUI(BatteryInfoSignal signal)
        {
            _panel.Show();
            _panel.PrepareUI(signal.BatteryMaxCharge, signal.BatteryCharge);
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<BatteryInfoSignal>(PrepareUI);
            _eventBus.Unsubscribe<BatteryChargeChangedSignal>(UpdateUI);
        }
    }
}