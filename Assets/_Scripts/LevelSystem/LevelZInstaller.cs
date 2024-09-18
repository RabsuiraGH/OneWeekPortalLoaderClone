using SceneFieldTools;
using UnityEngine;
using Zenject;

namespace Core.Level
{
    public class LevelZInstaller : MonoInstaller
    {

        [SerializeField] private SceneField _ingameMenuScene;

        [SerializeField] private LevelsDataSO _levelsData;
        public override void InstallBindings()
        {
            Container.Bind<LevelsDataSO>().FromInstance(_levelsData);
            Container.Bind<LevelManager>().AsSingle().NonLazy();
            Container.Bind<SceneField>().WithId("IngameMenu").FromInstance(_ingameMenuScene);
        }
    }
}