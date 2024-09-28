using Core.GameEventSystem;
using Core.GameEventSystem.Signals;
using Core.Utility.DebugTool;
using UnityEngine;
using Zenject;

namespace Core
{
    public class GlobalUIInput : MonoBehaviour
    {
        [SerializeField] private EventBus _eventBus = null;

        [SerializeField] private DebugLogger _debugger = new();

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);

            _eventBus.Subscribe<OpenCompletelyUISignal>(UIOpenedCompletely);
            _eventBus.Subscribe<CloseCompletelyUISignal>(UIClosedCompletely);
            _eventBus.Subscribe<SwitchWidowUISignal>(UIWindowSwitched);
        }

        private void UIOpenedCompletely(OpenCompletelyUISignal signal)
        {
            _debugger.Log(this, signal);

            _eventBus.Invoke(new SwitchToUIInputSignal());
            _eventBus.Invoke(new AnyActionUISignal());
        }

        private void UIClosedCompletely(CloseCompletelyUISignal signal)
        {
            _debugger.Log(this, signal);

            _eventBus.Invoke(new SwitchToGameplayInputSignal());
            _eventBus.Invoke(new AnyActionUISignal());
        }

        private void UIWindowSwitched(SwitchWidowUISignal signal)
        {
            _debugger.Log(this, signal);

            _eventBus.Invoke(new AnyActionUISignal());
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<OpenCompletelyUISignal>(UIOpenedCompletely);
            _eventBus.Unsubscribe<CloseCompletelyUISignal>(UIClosedCompletely);
            _eventBus.Unsubscribe<SwitchWidowUISignal>(UIWindowSwitched);
        }
    }
}