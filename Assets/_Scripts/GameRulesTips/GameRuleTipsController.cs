using System.Threading.Tasks;
using Core.GameEventSystem;
using Core.GameEventSystem.Signals;
using Core.GameRuleTips.UI;
using UnityEngine;
using Zenject;

namespace Core.GameRuleTips.Controller
{
    public class GameRuleTipsController : MonoBehaviour
    {
        [SerializeField] private GameObject _parent = null;

        [SerializeField] private GameRuleTipsPageUI _page = null;

        [SerializeField] private Canvas _canvas = null;

        [SerializeField] private EventBus _eventBus = null;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _canvas.worldCamera = Camera.main;

            _page.OnControlPageCloseButtonPressed += CloseControlPanel;
            _page.OnGoalPageClosedButtonPressed += CloseGoalPanel;
            _page.OnChargePageClosedButtonPressed += CloseChargePanel;

            _eventBus.Subscribe<BatteryLiftSignal>(ShowChargeTip);
            _eventBus.Subscribe<LevelLoadCompletedSignal>(ShowRules);
        }

        private void ShowRules(LevelLoadCompletedSignal signal)
        {
            if (!signal.FirstLoad)
            {
                RemoveRules();
                return;
            }

            _page.ToggleControlPanel(true);
            _eventBus.Invoke(new OpenCompletelyUISignal());
        }

        private async void CloseControlPanel()
        {
            _eventBus.Invoke(new CloseCompletelyUISignal());
            _page.ToggleControlPanel(false);

            await Task.Delay(200);

            _page.ToggleGoalPanel(true);
            _eventBus.Invoke(new OpenCompletelyUISignal());
        }

        private void CloseGoalPanel()
        {
            _page.ToggleGoalPanel(false);
            _eventBus.Invoke(new CloseCompletelyUISignal());
        }

        private void CloseChargePanel()
        {
            _page.ToggleChargePanel(false);
            _eventBus.Invoke(new CloseCompletelyUISignal());

            RemoveRules();
        }

        private void ShowChargeTip(BatteryLiftSignal signal)
        {
            _page.ToggleChargePanel(true);
            _eventBus.Invoke(new OpenCompletelyUISignal());
        }

        private void RemoveRules()
        {
            Destroy(_parent);
        }

        private void OnDestroy()
        {
            _page.OnControlPageCloseButtonPressed -= CloseControlPanel;
            _page.OnGoalPageClosedButtonPressed -= CloseGoalPanel;
            _page.OnChargePageClosedButtonPressed -= CloseChargePanel;
            _eventBus.Unsubscribe<BatteryLiftSignal>(ShowChargeTip);
            _eventBus.Unsubscribe<LevelLoadCompletedSignal>(ShowRules);

        }
    }
}