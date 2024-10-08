using Core.GameEventSystem;
using Core.Player.Movement;
using UnityEngine;
using Zenject;

namespace Core.Player.StateMachine
{
    public class PlayerState
    {
        [SerializeField] protected PlayerMain _player;
        [SerializeField] protected PlayerStateMachine _playerStateMachine;
        [SerializeField] protected EventBus _eventBus;

        public PlayerState(PlayerMain player, PlayerStateMachine playerStateMachine, EventBus eventBus)
        {
            _player = player;
            _playerStateMachine = playerStateMachine;
            _eventBus = eventBus;
        }

        public virtual void EnterState()
        { }

        public virtual void ExitState()
        { }

        public virtual void FrameUpdate()
        { }

        public virtual void PhysicsUpdate()
        { }

    }
}