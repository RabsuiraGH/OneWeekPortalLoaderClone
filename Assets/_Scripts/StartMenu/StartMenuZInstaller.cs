using Core.Level;
using Core.StartMenu;
using UnityEngine;
using Zenject;

namespace Core.StartMenu
{
    public class StartMenuZInstaller : MonoInstaller
    {

        [SerializeField] private LevelsDataSO _levelDataSO;

        [SerializeField] private LevelButtonUI _levelButtonPrefab;

        public override void InstallBindings()
        {
            Container.Bind<LevelsDataSO>().FromScriptableObject(_levelDataSO);
            Container.Bind<LevelButtonUI>().FromInstance(_levelButtonPrefab);

        }


    }
}
