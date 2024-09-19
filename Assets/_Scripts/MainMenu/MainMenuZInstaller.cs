using Core.MainMenu.UI;
using UnityEngine;
using Zenject;

namespace Core.MainMenu
{
    public class MainMenuZInstaller : MonoInstaller
    {
        [SerializeField] private LevelButtonUI _levelButtonPrefab = null;

        public override void InstallBindings()
        {
            Container.Bind<LevelButtonUI>().FromInstance(_levelButtonPrefab);
        }
    }
}