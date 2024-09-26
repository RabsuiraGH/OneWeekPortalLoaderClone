using UnityEngine;

namespace Core
{
    public class BeltMovementSignal
    {
        public readonly bool IsMoving = false;
        public readonly Vector2Int MovementDirection;

        public BeltMovementSignal(bool isMoving, Vector2Int movementDirection)
        {
            IsMoving = isMoving;
            MovementDirection = movementDirection;
        }
    }
}