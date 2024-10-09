using Core.CustomAnimationSystem;
using Core.GameEventSystem;
using Core.Player.Movement;
using Core.Player.StateMachine;
using UnityEngine;
using Zenject;

namespace Core.Player
{
    public class PlayerMain : MonoBehaviour
    {
        [field: SerializeField] private PlayerMovementController _movementController = null;

        [SerializeField] private PlayerStateMachine _stateMachine = null;

        [field: SerializeField] public PlayerIdleState IdleState = null;

        [field: SerializeField] public PlayerMovementState MovementState = null;

        [field: SerializeField] public CustomAnimator Animator { get; set; } = null;

        [SerializeField] private EventBus _eventBus;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            if (Animator == null)
                Animator = GetComponent<CustomAnimator>();

            _stateMachine = new();
            IdleState = new(
                this, _movementController, _stateMachine, _eventBus);

            MovementState = new(
                this, _movementController, _stateMachine, _eventBus);
        }

        private void Start()
        {
            _stateMachine.Initialize(IdleState);
        }

        private void Update()
        {
            _stateMachine.CurrentState.FrameUpdate();
        }
    }
}