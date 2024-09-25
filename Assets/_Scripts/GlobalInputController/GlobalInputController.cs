using Core.EventSystem;
using Core.EventSystem.Signals;
using Core.Input;
using Core.Utility.DebugTool;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

using static Core.Utility.DebugTool.DebugColorOptions.HtmlColor;

namespace Core.UI.GlobalController
{
    public class GlobalInputController : MonoBehaviour
    {
        [SerializeField] private BaseInput _baseInput = null;

        [SerializeField] private EventBus _eventBus = null;

        [SerializeField] private DebugLogger _debugger = new();

        [Inject]
        public void Construct(EventBus eventBus, BaseInput baseInput)
        {
            _eventBus = eventBus;
            _baseInput = baseInput;
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            _baseInput.Global.Escape.performed += OnEscapePressed;

            _eventBus.Subscribe<SwitchToUIInputSignal>(SwitchToUIInput);
            _eventBus.Subscribe<SwitchToGameplayInputSignal>(SwithToGameplayInput);
        }

        private void Start()
        {
            ApplyStartInputOptions();
        }

        [EasyButtons.Button]
        private void ApplyStartInputOptions()
        {
            _baseInput.Enable();

            _baseInput.Global.Enable();
            _baseInput.Gameplay.Disable();
            _baseInput.UI.Enable();
        }

        private void OnEscapePressed(InputAction.CallbackContext context)
        {
            _debugger.Log(this, context.action);

            _eventBus.Invoke(new EscapeCommandSignal());
        }

        private void SwithToGameplayInput(SwitchToGameplayInputSignal signal)
        {
            _debugger.Log(this, signal);

            _baseInput.Gameplay.Enable();
            _baseInput.UI.Disable();
        }

        private void SwitchToUIInput(SwitchToUIInputSignal signal)
        {
            _debugger.Log(this, signal);

            _baseInput.Gameplay.Disable();
            _baseInput.UI.Enable();
        }

        private void OnEnable()
        {
            _baseInput.Enable();
        }

        private void OnDisable()
        {
            _baseInput.Disable();
        }

        private void OnDestroy()
        {
            _baseInput.Global.Escape.performed -= OnEscapePressed;

            _eventBus.Unsubscribe<SwitchToUIInputSignal>(SwitchToUIInput);
            _eventBus.Unsubscribe<SwitchToGameplayInputSignal>(SwithToGameplayInput);
        }

#if UNITY_EDITOR

        private void Update()
        {
            _debugger.Log(this, ($"Innput State: Global: {(_baseInput.Global.enabled).Color(Bool)}  Gameplay: {(_baseInput.Gameplay.enabled).Color(Bool)}  UI: {(_baseInput.UI.enabled).Color(Bool)}"));
        }

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
    }
}