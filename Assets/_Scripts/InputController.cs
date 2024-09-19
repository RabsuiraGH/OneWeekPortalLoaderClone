using Core.EventSystem;
using Core.EventSystem.Signals;
using UnityEngine;
using Zenject;

namespace Core.Input
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private BaseInput _baseInput = null;
        [SerializeField] private EventBus _eventBus = null;

        [Inject]
        public void Construct(BaseInput baseInput, EventBus eventBus)
        {
            _baseInput = baseInput;
            _eventBus = eventBus;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);

            _eventBus.Subscribe<SwitchToUIInputSignal>(SwitchToUIInput);
            _eventBus.Subscribe<SwitchToGameplayInputSignal>(SwithToGameplayInput);
        }

#if UNITY_EDITOR

        private enum InputTypeDebug
        {
            Gameplay,
            UI,
        }

        [EasyButtons.Button]
        private void SwitchInput(InputTypeDebug type)
        {
            switch (type)
            {
                case InputTypeDebug.Gameplay:
                    SwithToGameplayInput(null);
                    break;

                case InputTypeDebug.UI:
                    SwitchToUIInput(null);
                    break;

                default:
                    break;
            }
        }

#endif

        private void SwithToGameplayInput(SwitchToGameplayInputSignal signal)
        {
            _baseInput.Gameplay.Enable();
            _baseInput.UI.Disable();
        }

        private void SwitchToUIInput(SwitchToUIInputSignal signal)
        {
            _baseInput.Gameplay.Disable();
            _baseInput.UI.Enable();
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<SwitchToUIInputSignal>(SwitchToUIInput);
            _eventBus.Unsubscribe<SwitchToGameplayInputSignal>(SwithToGameplayInput);
        }
    }
}