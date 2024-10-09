using Core.Level.Data;
using SceneFieldTools;
using UnityEngine;
using Zenject;

namespace Core.Level
{
    public class LevelZInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _ingameMenuScene = null;
        [SerializeField] private GameObject _levelCompletedMenuScene = null;
        [SerializeField] private GameObject _gameplayInterface = null;

        [SerializeField] private LevelsDataSO _levelsData = null;

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