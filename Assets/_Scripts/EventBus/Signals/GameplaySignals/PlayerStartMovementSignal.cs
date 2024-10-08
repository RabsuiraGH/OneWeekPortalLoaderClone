using Core.Utility;
using UnityEngine;

namespace Core.GameEventSystem.Signals
{
    public class PlayerStartMovementSignal
    {
        public readonly Directions.Direction movementDirection;
        public PlayerStartMovementSignal(Directions.Direction movementDirection)
        {
            this.movementDirection = movementDirection;
        }
    }
}
