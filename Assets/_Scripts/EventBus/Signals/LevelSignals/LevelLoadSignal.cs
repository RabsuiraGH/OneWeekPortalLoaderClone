namespace Core.GameEventSystem.Signals
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