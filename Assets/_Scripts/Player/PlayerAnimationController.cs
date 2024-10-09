using Core.CustomAnimationSystem;
using Core.Player.Movement;
using UnityEngine;
using Zenject;

namespace Core.Player.Animation
{
    public class PlayerAnimationController : CustomAnimator
    {
        [SerializeField] private IPerformMovement _player = null;

        [SerializeField] private PlayerIdleAnimation _idleAnimation = new();

        [SerializeField] private PlayerMovementAnimation _movementAnimation = new();

        [Inject]
        public void Construct(IPerformMovement player)
        {
            _player = player;
        }
    }
}