using System.Threading;
using System.Threading.Tasks;
using Core.EventSystem;
using Core.EventSystem.Signals;
using Core.Utility.DebugTool;
using UnityEngine;
using Zenject;

namespace Core.Player.Movement
{
    public class PlayerMovementController : MonoBehaviour, IMoveable
    {
        [SerializeField] private float _moveTime = 0.5f;

        [SerializeField] private Rigidbody2D _rigidBody = null;

        [SerializeField] private bool _isMoving = false;

        [SerializeField] private DebugLogger _debuger = new();

        [SerializeField] private EventBus _eventBus;

        [SerializeField] private LayerMask _wallLayer;

        private CancellationTokenSource _cancellationTokenSource;
        private Task _currentMoveTask;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async void Move(Vector2 direction)
        {
            if (_isMoving)
                return;

            if (!IsLegalMove(direction))
                return;

            _debuger.Log(this, "Movement performed");

            _isMoving = true;
            _currentMoveTask = MoveOverTimeAsync(_rigidBody.position, _rigidBody.position + direction, _moveTime, _cancellationTokenSource);
            await _currentMoveTask;
            _isMoving = false;
            _eventBus.Invoke(new PlayerMoveEndSignal());
        }

        public async void PlannedMovement(Vector2 direction)
        {
            if (_isMoving)
            {
                await _currentMoveTask;
            }
            Move(direction);
        }

        public bool IsLegalMove(Vector2 direction)
        {
            Collider2D collider = Physics2D.OverlapCircle(_rigidBody.position + direction, 0.1f, _wallLayer);

            if (collider != null)
                _debuger.Log(collider, "Illegal move, wall object: ", collider.gameObject);

            return collider == null;
        }

        private async Task MoveOverTimeAsync(Vector3 from, Vector3 to, float duration, CancellationTokenSource token)
        {
            from = Vector3Int.RoundToInt(from);
            to = Vector3Int.RoundToInt(to);

            float elapsedTime = 0;
            while (elapsedTime < duration)
            {
                if (token.IsCancellationRequested)
                    return;

                transform.position = Vector3.Lerp(from, to, elapsedTime / duration);
                elapsedTime += Time.deltaTime;

                await Task.Yield();
            }

            transform.position = to;
        }

        private void Awake()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _rigidBody = GetComponent<Rigidbody2D>();
            _eventBus.Subscribe<BeltMovementSignal>(BeltMovement);
        }

        private void BeltMovement(BeltMovementSignal signal)
        {
            _isMoving = signal.IsMoving;
            if (signal.MovementDirection == Vector2Int.zero) return;

            PlannedMovement(signal.MovementDirection);
        }

        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _eventBus.Unsubscribe<BeltMovementSignal>(BeltMovement);
        }
    }
}