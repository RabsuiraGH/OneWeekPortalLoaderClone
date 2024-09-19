namespace Core.EventSystem.Signals
{
    public class InputToggleSignal
    {
        public readonly bool Enable;

        public InputToggleSignal(bool enable)
        {
            Enable = enable;
        }
    }
}