using Core.EventSystem;
using Core.EventSystem.Signals;
using Core.Input;
using Core.Utility.VectorC;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Core.Input.UI
{
    public class UIInput : MonoBehaviour
    {
        [SerializeField] private EventBus _eventBus;

        [SerializeField] private BaseInput _baseInput;

        [Inject]
        public void Construct(EventBus eventbus, BaseInput baseInput)
        {
            _eventBus = eventbus;
            _baseInput = baseInput;
        }

        private void Awake()
        {
            _baseInput.UI.Navigation.started += Navigate;
            _baseInput.UI.Confirm.started += Confirm;
        }

        private void Confirm(InputAction.CallbackContext context)
        {
            _eventBus.Invoke(new ChooseOptionUISignal());
        }

        private void Navigate(InputAction.CallbackContext context)
        {
            Vector2 navigation = context.ReadValue<Vector2>().GetProperlyDirection();

            _eventBus.Invoke(new ChangeOptionUISignal(navigation));
        }

        private void OnDestroy()
        {
            _baseInput.UI.Navigation.started -= Navigate;
            _baseInput.UI.Confirm.started -= Confirm;
        }
    }
}