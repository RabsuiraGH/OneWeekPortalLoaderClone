using Core.GameEventSystem;
using Core.GameEventSystem.Signals;
using UnityEngine;
using Zenject;

namespace Core
{
    public class Battery : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider = null;

        [SerializeField] private string _playerTag = "Player";

        [SerializeField] private EventBus _eventBus = null;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag(_playerTag)) return;

            _eventBus.Invoke(new BatteryLiftSignal());

            Destroy(this.gameObject);
        }
    }
}