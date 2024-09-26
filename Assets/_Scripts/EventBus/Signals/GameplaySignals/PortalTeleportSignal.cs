using UnityEngine;

namespace Core.EventSystem.Signals
{
    public class PortalTeleportSignal
    {
        public readonly bool IsMoving = false;
        public readonly Vector2Int MovementDirection;
        public readonly Vector2Int TeleportPosition;

        public PortalTeleportSignal(bool isMoving, Vector2Int movementDirection, Vector2Int teleportPosition)
        {
            IsMoving = isMoving;
            MovementDirection = movementDirection;
            TeleportPosition = teleportPosition;
        }
    }
}