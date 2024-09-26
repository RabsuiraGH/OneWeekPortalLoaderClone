namespace Core.EventSystem.Signals
{
    public class BatteryChargeChangedSignal
    {
        public readonly int BatteryCharge;

        public BatteryChargeChangedSignal(int batteryCharge)
        {
            BatteryCharge = batteryCharge;
        }
    }
}