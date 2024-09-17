using UnityEngine;

namespace Core.Player.Movement
{
    public interface IMoveable
    {
        public void Move(Vector2 direction);
    }
}