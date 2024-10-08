using Core.Utility;
using UnityEngine;

namespace Core
{
    public class PlayerEndMovementSignal : MonoBehaviour
    {
        public readonly Directions.Direction movementDirection;
        public PlayerEndMovementSignal(Directions.Direction movementDirection)
        {
            this.movementDirection = movementDirection;
        }


    }
}
