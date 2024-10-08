using Core.CustomAnimationSystem;
using Core.Player.Movement;
using UnityEngine;
using Zenject;

namespace Core
{
    public class PlayerAnimationController : CustomAnimator
    {

        [SerializeField] private IPerformMovement _player;


        [SerializeField] private PlayerIdleAnimation _idleAnimation = new();

        [SerializeField] private PlayerMovementAnimation _movementAnimation = new();

        [Inject]
        public void Construct(IPerformMovement player)
        {
            _player = player;
        }



    }
}
