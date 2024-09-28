using UnityEngine;

namespace Core.EventSystem.Signals
{
    public class ChangeOptionUISignal
    {
        public readonly Vector2 direction;

        public ChangeOptionUISignal(Vector2 chooseDirection)
        {
            direction = chooseDirection;
        }
    }
}