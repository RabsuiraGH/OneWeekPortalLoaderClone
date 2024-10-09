using Core.GameEventSystem;
using Core.GameEventSystem.Signals;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Core.Gameplay.Object
{
    public class Accumulator : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider = null;

        [SerializeField] private string _playerTag = "Player";

        [SerializeField] private EventBus _eventBus = null;

        [SerializeField] private int _charge = 0;

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

            _eventBus.Invoke(new AccumulatorLiftSignal(_charge));

            Destroy(this.gameObject);
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<BatteryLiftSignal>(OnBatteryLifted);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 20;
            Handles.Label(transform.position, $"{_charge}", style);
        }
#endif
    }
}