using Core.EventSystem;
using UnityEngine;
using Zenject;

namespace Core.EventSystem
{
    public class EventBusZInstaller : MonoInstaller
    {
        [SerializeField] private EventBus _eventBus;

        public override void InstallBindings()
        {
            Container.Bind<EventBus>().FromInstance(_eventBus);
        }
    }
}