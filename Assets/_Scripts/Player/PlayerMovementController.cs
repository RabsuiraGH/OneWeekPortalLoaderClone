using System.Threading;
using System.Threading.Tasks;
using Core.GameEventSystem;
using Core.GameEventSystem.Signals;
using Core.Utility;
using Core.Utility.DebugTool;
using UnityEngine;
using Zenject;

namespace Core.Player.Movement
{
    public class PlayerMovementController : MonoBehaviour, IPerformMovement
    {
        [SerializeField] private float _moveTime = 0.5f;

        [SerializeField] private Rigidbody2D _rigidBody = null;

        [field: SerializeField] public bool IsMoving { get; private set; } = false;

        [field: SerializeField] public Directions.Direction FaceDirection { get; private set; }

        [SerializeField] private DebugLogger _debuger = new();

        [SerializeField] private LayerMask _wallLayer;

        private CancellationTokenSource _cancellationTokenSource;
        private Task _currentMoveTask;

        [SerializeField] private EventBus _eventBus;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            _rigidBody = GetComponent<Rigidbody2D>();

            _eventBus.Subscribe<BeltMovementSignal>(BeltMovement);
            _eventBus.Subscribe<PortalTeleportSignal>(PortalMovement);
        }

        public async void InputMove(Vector2 direction)
        {
            if (!CanPerformMovement(direction)) return;

            PerformMovement(direction, true);

            _eventBus.Invoke(new PlayerInputMoveStartSignal());

            await _currentMoveTask;

            _eventBus.Invoke(new PlayerInputMoveEndSignal());
        }

        public void ThirdPartyMove(Vector2 direction, bool changeFaceDirection)
        {
            if (!CanPerformMovement(direction)) return;

            PerformMovement(direction, changeFaceDirection);
        }

        public bool CanPerformMovement(Vector2 direction)
        {
            return !IsMoving || IsLegalMove(direction);
        }

        public bool IsLegalMove(Vector2 direction)
        {
            Collider2D collider = Physics2D.OverlapCircle(_rigidBody.position + direction, 0.1f, _wallLayer);

            if (collider != null)
                _debuger.Log(collider, "Illegal move, wall object: ", collider.gameObject);

            return collider == null;
        }

        public async void PlannedMovement(Vector2 direction, bool changeFaceDirection)
        {
            if (IsMoving)
            {
                await _currentMoveTask;
            }
            PerformMovement(direction, changeFaceDirection);
        }

        private async void PerformMovement(Vector2 direction, bool changeFaceDirection)
        {
            _debuger.Log(this, "Movement performed");

            if (changeFaceDirection)
                FaceDirection = direction.GetDirection();

            IsMoving = true;
            _currentMoveTask = Move(_rigidBody.position, _rigidBody.position + direction, _moveTime, _cancellationTokenSource);
            await _currentMoveTask;
            IsMoving = false;
        }

        private async Task Move(Vector3 from, Vector3 to, float duration, CancellationTokenSource token)
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

        private async void PortalMovement(PortalTeleportSignal signal)
        {
            _rigidBody.position = signal.TeleportPosition;

            if (signal.MovementDirection == Vector2Int.zero) return;

            PlannedMovement(signal.MovementDirection, true);

            await _currentMoveTask;

            _eventBus.Invoke(new PortalTeleportEndSignal());
        }

        private void BeltMovement(BeltMovementSignal signal)
        {
            IsMoving = signal.IsMoving;
            if (signal.MovementDirection == Vector2Int.zero) return;

            PlannedMovement(signal.MovementDirection, false);
        }

        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _eventBus.Unsubscribe<BeltMovementSignal>(BeltMovement);
            _eventBus.Unsubscribe<PortalTeleportSignal>(PortalMovement);
        }
    }
}