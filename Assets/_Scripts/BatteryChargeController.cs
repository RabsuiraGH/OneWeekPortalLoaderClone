using Core.EventSystem;
using Core.EventSystem.Signals;
using Core.Utility.DebugTool;
using UnityEngine;
using Zenject;

namespace Core
{
    public class BatteryChargeController : MonoBehaviour
    {
        [SerializeField] private int _batteryCharges = 0;

        [SerializeField] private int _maximumBatteryCharges = 10;

        [SerializeField] private EventBus _eventBus = null;

        [SerializeField] private bool _isBatteeryLifted = false;


        [SerializeField] private DebugLogger _debuger = new();

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _batteryCharges = _maximumBatteryCharges;
            _eventBus.Subscribe<BatteryLiftSignal>(OnBatteryLift);
        }

        private void OnBatteryLift(BatteryLiftSignal batteryLiftSignal)
        {
            _isBatteeryLifted = true;
            _eventBus.Subscribe<PlayerMoveSignal>(OnPlayerMove);
        }

        private void OnPlayerMove(PlayerMoveSignal playerMoveSignal)
        {
            if (!_isBatteeryLifted) return;

            _batteryCharges -= 1;
            _debuger.Log(this, $"Current battery charge: {_batteryCharges}");

            if (_batteryCharges <= 0)
            {
                _debuger.Log(this, $"Battery have no charges!");

                _eventBus.Invoke(new GameOverSignal());
            }
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<BatteryLiftSignal>(OnBatteryLift);
            _eventBus.Unsubscribe<PlayerMoveSignal>(OnPlayerMove);

        }
    }
}