using Core.GameEventSystem;
using Core.GameEventSystem.Signals;
using UnityEngine;
using Zenject;

namespace Core.Gameplay.Object
{
    public class BatteryHolder : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider = null;

        [SerializeField] private string _playerTag = "Player";

        [SerializeField] private EventBus _eventBus = null;

        [SerializeField] private bool _isBatteeryLifted = false;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();

            _eventBus.Subscribe<BatteryLiftSignal>(OnBatteryLifted);
        }

        private void OnBatteryLifted(BatteryLiftSignal signal)
        {
            _isBatteeryLifted = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag(_playerTag)) return;
            if (!_isBatteeryLifted) return;

            _eventBus.Invoke(new LevelCompletedSignal());
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<BatteryLiftSignal>(OnBatteryLifted);
        }
    }
}