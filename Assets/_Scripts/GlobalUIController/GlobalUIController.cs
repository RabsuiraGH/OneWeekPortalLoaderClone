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
        public void Construct(EventBus eventBus, BaseInput baseInput)
        {
            _eventBus = eventBus;
            _baseInput = baseInput;
        }

        private void Awake()
        {
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