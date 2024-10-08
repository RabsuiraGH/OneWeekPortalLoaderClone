using System;
using UnityEngine;

namespace Core.Player.StateMachine
{
    [Serializable]
    public class PlayerStateMachine
    {
        [field: SerializeField] public PlayerState CurrentState { get; set; }

        public void Initialize(PlayerState startingState)
        {
            CurrentState = startingState;
            CurrentState.EnterState();
        }

        public void ChangeState(PlayerState newState)
        {
            CurrentState.ExitState();
            CurrentState = newState;
            CurrentState.EnterState();
        }
    }
}