namespace Core.EventSystem.Signals
{
    public class AccumulatorLiftSignal
    {
        public readonly int Charge;

        public AccumulatorLiftSignal(int charge)
        {
            Charge = charge;
        }
    }
}