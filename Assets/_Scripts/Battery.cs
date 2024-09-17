using Core.EventSystem;
using Core.EventSystem.Signals;
using UnityEngine;
using Zenject;

namespace Core
{
    public class Battery : MonoBehaviour
    {

        [SerializeField] private Collider2D _collider;

        [SerializeField] private string _playerTag;

        [SerializeField] private EventBus _eventBus;

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
