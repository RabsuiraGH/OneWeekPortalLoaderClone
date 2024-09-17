using System.Threading.Tasks;
using UnityEngine;

namespace Core.Player.Movement
{
    public class PlayerMovementController : MonoBehaviour, IMoveable
    {
        [SerializeField] private Vector2 _moveDirection;

        [SerializeField] private float _moveTime;
        [SerializeField] private float _moveTimeCounter;

        [SerializeField] private Rigidbody2D _rigidBody;

        [SerializeField] private bool _isMoving;

        public async void Move(Vector2 direction)
        {
            if (_isMoving)
                return;

            _isMoving = true;
            await MoveOverTimeAsync(_rigidBody.position, _rigidBody.position + direction, 0.5f);
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

        private void Update()
        {
        }
    }
}