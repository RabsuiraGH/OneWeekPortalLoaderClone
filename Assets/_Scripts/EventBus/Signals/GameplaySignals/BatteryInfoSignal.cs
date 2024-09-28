namespace Core.GameEventSystem.Signals
{
    public class BatteryInfoSignal
    {
        public readonly int BatteryMaxCharge;
        public readonly int BatteryCharge;

        public BatteryInfoSignal(int batteryMaxCharge, int batteryChaarge)
        {
            BatteryMaxCharge = batteryMaxCharge;
            BatteryCharge = batteryChaarge;
        }
    }
}