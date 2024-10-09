using Core.Utility;

namespace Core.GameEventSystem.Signals
{
    public class PlayerStartMovementSignal
    {
        public readonly Directions.Direction MovementDirection;

        public PlayerStartMovementSignal(Directions.Direction movementDirection)
        {
            this.MovementDirection = movementDirection;
        }
    }
}