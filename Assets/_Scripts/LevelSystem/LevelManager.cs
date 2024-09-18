using System;
using System.Collections.Generic;
using SceneFieldTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.Level
{
    [Serializable]
    public class LevelManager
    {
        [Inject(Id = "IngameMenu")]
        [SerializeField] private SceneField _ingameMenuScene = null;

        [SerializeField] private LevelsDataSO _levelsData;

        [Inject]
        public LevelManager(LevelsDataSO levelsData)
        {
            _levelsData = levelsData;
        }

        public IEnumerable<LevelData> GetLevelsData()
        {
            return _levelsData.GetLevelList();
        }

        public void LoadLevel(int levelIndex)
        {
            LoadLevel(_levelsData.GetLevelAt(levelIndex));
        }

        private void LoadLevel(LevelData level)
        {
            SceneManager.LoadScene(level.LevelScene);
            SceneManager.LoadScene(_ingameMenuScene, LoadSceneMode.Additive);
        }
    }
}