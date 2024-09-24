using System;
using System.Collections.Generic;
using Core.EventSystem;
using SceneFieldTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.Level
{
    [Serializable]
    public class LevelManager
    {
        [Inject(Id = Zenject.ZenjectIDs.IngameMenu)]
        [SerializeField] private SceneField _ingameMenuScene = null;

        [Inject(Id = Zenject.ZenjectIDs.LevelCompletedMenu)]
        [SerializeField] private SceneField _levelCompletedMenu = null;

        [SerializeField] private LevelsDataSO _levelsData;

        [SerializeField] private EventBus _eventBus;

        [Inject]
        public LevelManager(LevelsDataSO levelsData, EventBus eventBus)
        {
            _levelsData = levelsData;
            _eventBus = eventBus;
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
            SceneManager.LoadScene(_levelCompletedMenu, LoadSceneMode.Additive);
        }
    }
}