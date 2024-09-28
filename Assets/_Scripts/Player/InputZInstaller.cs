using Zenject;

namespace Core.Input
{
    public class InputZInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BaseInput>().AsSingle().NonLazy();
        }
    }
}