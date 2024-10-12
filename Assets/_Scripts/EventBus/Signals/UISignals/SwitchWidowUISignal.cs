using UnityEngine;

namespace Core.GameEventSystem.Signals
{
    public class SwitchWidowUISignal
    {
        public readonly Transform FromUI;
        public readonly Transform ToUI;

        public SwitchWidowUISignal(Transform fromUI, Transform toUI)
        {
            FromUI = fromUI;
            ToUI = toUI;
        }
    }
}