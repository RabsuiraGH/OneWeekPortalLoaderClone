namespace Core.GameEventSystem.Signals
{
    public class LevelLoadCompletedSignal
    {
        public readonly int LevelIndex;
        public readonly bool FirstLoad;

        public LevelLoadCompletedSignal(int levelIndex, bool firstLoad)
        {
            LevelIndex = levelIndex;
            FirstLoad = firstLoad;
        }
    }
}