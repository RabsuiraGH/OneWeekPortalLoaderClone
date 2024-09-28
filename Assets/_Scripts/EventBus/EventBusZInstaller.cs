using Zenject;

namespace Core.GameEventSystem
{
    public class EventBusZInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EventBus>().AsSingle().NonLazy();
        }
    }
}