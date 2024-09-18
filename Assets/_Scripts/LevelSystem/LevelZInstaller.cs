using SceneFieldTools;
using UnityEngine;
using Zenject;

namespace Core.Level
{
    public class LevelZInstaller : MonoInstaller
    {

        [SerializeField] private SceneField _ingameMenuScene;
        public override void InstallBindings()
        {
            Container.Bind<LevelLoader>().AsSingle().NonLazy();
            Container.Bind<SceneField>().WithId("IngameMenu").FromInstance(_ingameMenuScene);
        }
    }
}