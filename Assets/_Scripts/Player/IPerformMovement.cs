using Core.Utility;
using UnityEngine;

namespace Core.Player.Movement
{
    public interface IPerformMovement
    {
        public bool IsMoving { get; }

        public Directions.Direction FaceDirection { get; }

        public void InputMove(Vector2 direction);

        public void ThirdPartyMove(Vector2 direction, bool changeFaceDirection);
    }
}