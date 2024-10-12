using UnityEngine;

namespace Core.GameEventSystem.Signals
{
    public class OpenUISignal
    {
        public readonly Transform UI;
        public OpenUISignal(Transform UI)
        {
            this.UI = UI;
        }
    }
}