using Core.GameEventSystem;
using Core.GameEventSystem.Signals;
using UnityEngine;
using Zenject;

namespace Core.Gameplay.Object
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider = null;

        [SerializeField] private EventBus _eventBus = null;

        [SerializeField] private Portal _sibling = null;

        [field: SerializeField] public Vector2Int ExitDirection { get; private set; } = Vector2Int.zero;

        [SerializeField] private bool _requireTeleport = false;

        [SerializeField] private Collider2D _objectToTeleport = null;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _eventBus.Subscribe<PlayerEndMovementSignal>(Teleport);
            _eventBus.Subscribe<PortalTeleportEndSignal>(ResetTeleport);
        }

        private void ResetTeleport(PortalTeleportEndSignal signal)
        {
            _collider.enabled = true;
        }

        public void TreatSiblingAsExit()
        {
            _collider.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;

            _requireTeleport = true;
            _objectToTeleport = collision;
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<PlayerEndMovementSignal>(Teleport);
            _eventBus.Unsubscribe<PortalTeleportEndSignal>(ResetTeleport);
        }

        private void Teleport(PlayerEndMovementSignal signal)
        {
            if (!_requireTeleport) return;

            _sibling.TreatSiblingAsExit();

            _eventBus.Invoke(new PortalTeleportSignal(true, _sibling.ExitDirection, Vector2Int.RoundToInt(_sibling.transform.position)));

            _requireTeleport = false;
            _objectToTeleport = null;
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)(Vector2)ExitDirection * 0.5f);
            Gizmos.DrawWireSphere(transform.position, 0.25f);
        }
#endif
    }
}