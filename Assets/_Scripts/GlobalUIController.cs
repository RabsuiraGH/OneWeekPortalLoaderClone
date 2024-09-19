using Core.EventSystem;
using Core.EventSystem.Signals;
using UnityEngine;
using Zenject;

namespace Core
{
    public class GlobalUIController : MonoBehaviour
    {
        [SerializeField] private EventBus _eventBus;

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
            _eventBus.Invoke(new SwitchToUIInputSignal());
        }

        private void UIClosedCompletely(CloseCompletelyUISignal signal)
        {
            _eventBus.Invoke(new SwitchToGameplayInputSignal());
        }

        private void UIWindowSwitched(SwitchWidowUISignal signal)
        {
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<OpenCompletelyUISignal>(UIOpenedCompletely);
            _eventBus.Unsubscribe<CloseCompletelyUISignal>(UIClosedCompletely);
            _eventBus.Unsubscribe<SwitchWidowUISignal>(UIWindowSwitched);
        }
    }
}