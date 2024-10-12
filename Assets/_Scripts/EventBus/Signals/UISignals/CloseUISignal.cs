using UnityEngine;

namespace Core.GameEventSystem.Signals
{
    public class CloseUISignal
    {
        public readonly Transform UI;
        public CloseUISignal(Transform UI)
        {
            this.UI = UI;
        }
    }
}