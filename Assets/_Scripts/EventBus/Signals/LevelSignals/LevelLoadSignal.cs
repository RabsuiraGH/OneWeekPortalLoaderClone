namespace Core.EventSystem.Signals
{
    public class LevelLoadSignal
    {
        public readonly int LevelIndex;

        public LevelLoadSignal(int levelIndex)
        {
            LevelIndex = levelIndex;
        }
    }
}