using Core.GameEventSystem;
using Core.GameEventSystem.Signals;
using Core.Player.Movement;
using UnityEngine;

namespace Core.Player.StateMachine
{
    public class PlayerIdleState : PlayerState
    {
        [SerializeField] private PlayerIdleAnimation _idleAnimation = new();

        [SerializeField] private IPerformMovement _movementController;

        public PlayerIdleState(PlayerMain player,IPerformMovement movementController, PlayerStateMachine playerStateMachine, EventBus eventBus) : base(player, playerStateMachine, eventBus)
        {
            _movementController = movementController;
        }

        public override void EnterState()
        {
            _idleAnimation.SetTags(_movementController.FaceDirection.ToString());

            _player.Animator.PlayAnimation(_idleAnimation);


        }

        public override void ExitState()
        {
        }

        public override void FrameUpdate()
        {
            if(_movementController.IsMoving)
            {
                _playerStateMachine.ChangeState(_player.MovementState);
            }
        }

        public override void PhysicsUpdate()
        {
        }
    }
}
