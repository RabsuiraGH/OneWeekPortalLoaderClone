using Zenject;

namespace Core.EventSystem
{
    public class EventBusZInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EventBus>().AsSingle().NonLazy();
        }
    }
}