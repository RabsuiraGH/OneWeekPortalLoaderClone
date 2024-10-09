using Core.Utility;

namespace Core.GameEventSystem.Signals
{
    public class PlayerEndMovementSignal
    {
        public readonly Directions.Direction MovementDirection;

        public PlayerEndMovementSignal(Directions.Direction movementDirection)
        {
            MovementDirection = movementDirection;
        }
    }
}