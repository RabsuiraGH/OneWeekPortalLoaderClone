using Core.EventSystem;
using Core.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Core.UI.GlobalController
{
    public class GlobalUIController : MonoBehaviour
    {
        [SerializeField] private BaseInput _baseInput;

        [SerializeField] private EventBus _eventBus;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _baseInput = new();
            DontDestroyOnLoad(gameObject);

            _baseInput.UI.Escape.performed += OnEscapePressed;
        }

        private void OnEscapePressed(InputAction.CallbackContext context)
        {
            _eventBus.Invoke(new EscapeCommandUISignal());
        }

        private void OnEnable()
        {
            _baseInput.Enable();
        }

        private void OnDisable()
        {
            _baseInput.Disable();
        }
    }
}