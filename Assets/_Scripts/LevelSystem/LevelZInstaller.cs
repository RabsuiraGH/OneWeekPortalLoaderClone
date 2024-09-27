using SceneFieldTools;
using UnityEngine;
using Zenject;

namespace Core.Level
{
    public class LevelZInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _ingameMenuScene;
        [SerializeField] private GameObject _levelCompletedMenuScene;
        [SerializeField] private GameObject _gameplayInterface;

        [SerializeField] private LevelsDataSO _levelsData;

        public override void InstallBindings()
        {
            Container.Bind<LevelsDataSO>().FromInstance(_levelsData);
            Container.Bind<LevelManager>().AsSingle().NonLazy();
            Container.Bind<GameObject>().WithId(Zenject.ZenjectIDs.IngameMenu).FromInstance(_ingameMenuScene);
            Container.Bind<GameObject>().WithId(Zenject.ZenjectIDs.LevelCompletedMenu).FromInstance(_levelCompletedMenuScene);
            Container.Bind<GameObject>().WithId(Zenject.ZenjectIDs.GameplayInterface).FromInstance(_gameplayInterface);
        }
    }
}