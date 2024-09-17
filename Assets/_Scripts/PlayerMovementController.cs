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

        [SerializeField] private DebugLogger _debugger = new();

        [SerializeField] private EventBus _eventBus;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async void Move(Vector2 direction)
        {
            if (_isMoving)
                return;

            _debugger.Log(this, "Movement performed");

            _eventBus.Invoke(new PlayerMoveSignal());

            _isMoving = true;
            await MoveOverTimeAsync(_rigidBody.position, _rigidBody.position + direction, _moveTime);
            _isMoving = false;
        }

        private async Task MoveOverTimeAsync(Vector3 from, Vector3 to, float duration)
        {
            float elapsedTime = 0;
            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(from, to, elapsedTime / duration);
                elapsedTime += Time.deltaTime;

                await Task.Yield();
            }

            transform.position = to;
        }

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }
    }
}