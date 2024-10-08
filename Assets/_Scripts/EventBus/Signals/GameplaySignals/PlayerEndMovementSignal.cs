using Core.Utility;

namespace Core.GameEventSystem.Signals
{
    public class PlayerEndMovementSignal
    {
        public readonly Directions.Direction movementDirection;

        public PlayerEndMovementSignal(Directions.Direction movementDirection)
        {
            this.movementDirection = movementDirection;
        }
    }
}