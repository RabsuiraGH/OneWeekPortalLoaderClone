using System.Threading;
using System.Threading.Tasks;
using Core.Input;
using Core.Player.Movement;
using Core.Utility.VectorC;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Core.Input.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private BaseInput _baseInput = null;

        [SerializeField] private IMoveable _player = null;

        [SerializeField] private bool _isHoldingMovement = false;

        [SerializeField] private float _repeatRate = 0.75f;

        private CancellationTokenSource _cancellationTokenSource = null;

        [Inject]
        public void Construct(IMoveable player, BaseInput baseInput)
        {
            _player = player;
            _baseInput = baseInput;
        }

        private void Awake()
        {
            _baseInput.Gameplay.Movement.started += OnMovementStarted;
            _baseInput.Gameplay.Movement.canceled += OnMovementCanceled;
        }

        private void OnMovementStarted(InputAction.CallbackContext context)
        {
            if (_isHoldingMovement)
                return;

            _cancellationTokenSource = new CancellationTokenSource();

            _isHoldingMovement = true;
            HoldMovementAsync(context, _cancellationTokenSource); // Запуск асинхронного метода
        }

        private void OnMovementCanceled(InputAction.CallbackContext context)
        {
            _isHoldingMovement = false;
            _cancellationTokenSource.Cancel();
        }

        private async void HoldMovementAsync(InputAction.CallbackContext context, CancellationTokenSource token)
        {
            while (_isHoldingMovement)
            {
                if (token.IsCancellationRequested)
                    return;

                PerformMovement(context);

                await Task.Delay((int)(_repeatRate * 1000));
            }
        }

        private void PerformMovement(InputAction.CallbackContext context)
        {
            Vector2 direction = context.ReadValue<Vector2>().GetProperlyDirection();

            if (direction == Vector2.zero) return;

            _player.Move(direction);
        }


        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();

            _baseInput.Gameplay.Movement.started -= OnMovementStarted;
            _baseInput.Gameplay.Movement.canceled -= OnMovementCanceled;
        }
    }
}