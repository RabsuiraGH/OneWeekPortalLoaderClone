using System.Collections.Generic;
using Core.GameEventSystem;
using Core.GameEventSystem.Signals;
using Core.Utility.DebugTool;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.UI.GlobalController
{
    public class GlobalUIController : MonoBehaviour
    {
        [SerializeField] private EventBus _eventBus = null;

        [SerializeField] private DebugLogger _debugger = new();

        [SerializeField] private List<Transform> _activeUIList = new();

        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);

            SceneManager.sceneLoaded += ClearUI;

            _eventBus.Subscribe<OpenUISignal>(UIOpened);
            _eventBus.Subscribe<CloseUISignal>(UIClosed);
            _eventBus.Subscribe<SwitchWidowUISignal>(UIWindowSwitched);
        }

        private void ClearUI(Scene scene, LoadSceneMode mode)
        {
            if (mode is LoadSceneMode.Single)
            {
                _activeUIList.Clear();
            }
        }

        private void UIOpened(OpenUISignal signal)
        {
            _debugger.Log(this, signal);

            if (signal.UI != null)
                _activeUIList.Add(signal.UI);

            _eventBus.Invoke(new SwitchToUIInputSignal());
        }

        private void UIClosed(CloseUISignal signal)
        {
            _debugger.Log(this, signal);

            if (_activeUIList.Contains(signal.UI))
            {
                _activeUIList.Remove(signal.UI);
            }
            if (_activeUIList.Count <= 0)
            {
                _eventBus.Invoke(new SwitchToGameplayInputSignal());
            }
        }

        private void UIWindowSwitched(SwitchWidowUISignal signal)
        {
            _debugger.Log(this, signal);
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= ClearUI;

            _eventBus.Unsubscribe<OpenUISignal>(UIOpened);
            _eventBus.Unsubscribe<CloseUISignal>(UIClosed);
            _eventBus.Unsubscribe<SwitchWidowUISignal>(UIWindowSwitched);
        }
    }
}