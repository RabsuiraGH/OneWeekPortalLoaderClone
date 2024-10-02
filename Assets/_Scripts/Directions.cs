using UnityEngine;

namespace Core.Utility
{
    public static class Directions
    {
        public enum Direction
        {
            Up, Down, Left, Right, Zero, Angle
        }

        public static Direction GetDirection(this Vector2Int vector)
        {
            return ((Vector2)vector).GetDirection();
        }

        public static Direction GetDirection(this Vector2 vector)
        {
            if (vector == Vector2.zero)
            {
                return Direction.Zero;
            }

            if (vector.y > 0 && vector.x == 0)
            {
                return Direction.Up;
            }

            if (vector.y < 0 && vector.x == 0)
            {
                return Direction.Down;
            }

            if (vector.x < 0 && vector.y == 0)
            {
                return Direction.Left;
            }

            if (vector.x > 0 && vector.y == 0)
            {
                return Direction.Right;
            }

            return Direction.Angle;
        }
    }
}