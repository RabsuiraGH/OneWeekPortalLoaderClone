using Core.GameEventSystem.Signals;
using Core.Player.Movement;
using Core.Utility;
using Unity.VisualScripting;
using UnityEngine;

namespace Core.Player.StateMachine
{
    public class PlayerMovementState : PlayerState
    {

        [SerializeField] private PlayerMovementAnimation _movementAnimation = new();


        [SerializeField] private Directions.Direction  _movementDirection;

        [SerializeField] private IPerformMovement _movementController;


        public PlayerMovementState(PlayerMain player, IPerformMovement movementController, PlayerStateMachine playerStateMachine, GameEventSystem.EventBus eventBus) : base(player, playerStateMachine, eventBus)
        {
            _movementController = movementController;
        }

        public override void EnterState()
        {
            _movementDirection = _movementController.FaceDirection;

            _movementAnimation.SetTags(_movementDirection.ToString());
            _player.Animator.PlayAnimation(_movementAnimation);

            _eventBus.Invoke(new PlayerStartMovementSignal(_movementDirection));
        }

        public override void ExitState()
        {
            _eventBus.Invoke(new PlayerEndMovementSignal(_movementDirection));
        }

        public override void FrameUpdate()
        {
            if (!_movementController.IsMoving)
            {
                _playerStateMachine.ChangeState(_player.IdleState);
            }
        }

        public override void PhysicsUpdate()
        {
        }
    }
}
