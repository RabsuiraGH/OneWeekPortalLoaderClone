using Core.Player.Movement;
using UnityEngine;
using Zenject;

namespace Core.Player
{
    public class PlayerZInstaller : MonoInstaller
    {
        [SerializeField] private PlayerMovementController _player;

        public override void InstallBindings()
        {
            Container.Bind<IPerformMovement>().FromInstance(_player);
        }
    }
}