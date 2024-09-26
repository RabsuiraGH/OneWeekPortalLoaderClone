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

        [SerializeField] private bool _lockFailure = false;

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
            _eventBus.Subscribe<LevelCompletedSignal>(LockFailure);
            _eventBus.Subscribe<AccumulatorLiftSignal>(ChargeBattery);
        }

        private void ChargeBattery(AccumulatorLiftSignal signal)
        {
            _batteryCharges += signal.Charge;

            if (_batteryCharges > _maximumBatteryCharges)
            {
                _batteryCharges = _maximumBatteryCharges;
            }
        }

        private void LockFailure(LevelCompletedSignal signal)
        {
            _lockFailure = true;
        }

        private void OnBatteryLift(BatteryLiftSignal batteryLiftSignal)
        {
            _isBatteeryLifted = true;
            _eventBus.Invoke(new BatteryInfoSignal(_maximumBatteryCharges, _batteryCharges));
            _eventBus.Subscribe<PlayerMoveEndSignal>(OnPlayerMove);
        }

        private void OnPlayerMove(PlayerMoveEndSignal playerMoveSignal)
        {
            if (!_isBatteeryLifted) return;

            _batteryCharges -= 1;
            _debuger.Log(this, $"Current battery charge: {_batteryCharges}");

            _eventBus.Invoke(new BatteryChargeChangedSignal(_batteryCharges));

            if (_batteryCharges <= 0)
            {
                _debuger.Log(this, $"Battery have no charges!");

                if (!_lockFailure)
                {
                    _eventBus.Invoke(new GameOverSignal());
                }
            }
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<BatteryLiftSignal>(OnBatteryLift);
            if (_isBatteeryLifted)
                _eventBus.Unsubscribe<PlayerMoveEndSignal>(OnPlayerMove);
            _eventBus.Unsubscribe<AccumulatorLiftSignal>(ChargeBattery);
        }
    }
}