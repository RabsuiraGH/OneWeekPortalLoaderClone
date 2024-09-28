using Core.Input;
using Core.Player.Movement;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Core.Player.Input
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private BaseInput _baseInput;

        [SerializeField] private IMoveable _player;

        [Inject]
        public void Construct(IMoveable player, BaseInput baseInput)
        {
            _player = player;
            _baseInput = baseInput;
        }

        private void Awake()
        {
            _baseInput.Gameplay.Movement.started += PerformMovement;
        }

        private void PerformMovement(InputAction.CallbackContext context)

        {
            Vector2 direction = GetProperlyDirection(context.ReadValue<Vector2>());

            if (direction == Vector2.zero) return;

            _player.Move(direction);
        }

        public Vector2 GetProperlyDirection(Vector2 direction)
        {
            if (direction == Vector2.zero) return Vector2.zero;

            if (direction.y > 0) return Vector2.up;
            if (direction.y < 0) return Vector2.down;
            if (direction.x < 0) return Vector2.left;
            if (direction.x > 0) return Vector2.right;

            return Vector2.zero;
        }

        private void OnDestroy()
        {
            _baseInput.Gameplay.Movement.started -= PerformMovement;
        }
    }
}