using System.Collections.Generic;
using UnityEngine;

namespace Core.Level
{
    [CreateAssetMenu(fileName = "LevelsDataSO", menuName = "Game/LevelsDataSO")]
    public class LevelsDataSO : ScriptableObject
    {
        [SerializeField] private List<LevelData> _levels = new();

        public LevelData GetLevelAt(int index)
        {
            if(index >= _levels.Count || index < 0) return null;
            return _levels[index];
        }

        public IEnumerable<LevelData> GetLevelList()
        {
            return new List<LevelData>(_levels);
        }
    }
}