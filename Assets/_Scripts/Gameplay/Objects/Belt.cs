using Core.CustomAnimationSystem;
using Core.GameEventSystem;
using Core.Utility;
using UnityEngine;
using Zenject;

namespace Core
{
    public class Belt : MonoBehaviour
    {
        [SerializeField] private EventBus _eventBus;

        [SerializeField] private Vector2Int _direction;

        [SerializeField] private Collider2D _collider;

        [SerializeField] private CustomAnimator _animator;

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

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)(Vector2)_direction * 0.5f);
            Gizmos.DrawWireSphere(transform.position, 0.25f);
        }
    }
}