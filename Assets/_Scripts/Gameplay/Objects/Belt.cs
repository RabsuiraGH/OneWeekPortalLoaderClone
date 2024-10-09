using Core.CustomAnimationSystem;
using Core.GameEventSystem;
using Core.GameEventSystem.Signals;
using Core.Utility;
using UnityEngine;
using Zenject;

namespace Core.Gameplay.Object
{
    public class Belt : MonoBehaviour
    {
        [SerializeField] private EventBus _eventBus = null;

        [SerializeField] private Vector2Int _direction = Vector2Int.zero;

        [SerializeField] private Collider2D _collider = null;

        [SerializeField] private CustomAnimator _animator = null;

        [SerializeField] private BeltMovementAnimation _movementAnimation = new();

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _animator = GetComponent<CustomAnimator>();

            _movementAnimation.SetTags(_direction.GetDirection().ToString());
            _animator.PlayAnimation(_movementAnimation);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;

            _eventBus.Invoke(new BeltMovementSignal(true, _direction));
        }
#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)(Vector2)_direction * 0.5f);
            Gizmos.DrawWireSphere(transform.position, 0.25f);
        }
#endif
    }
}