using System;
using System.Collections.Generic;
using Core.EventSystem;
using Core.EventSystem.Signals;
using SceneFieldTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.Level
{
    [Serializable]
    public class LevelManager : IDisposable
    {
        [Inject(Id = Zenject.ZenjectIDs.IngameMenu)]
        [SerializeField] private SceneField _ingameMenuScene = null;

        [Inject(Id = Zenject.ZenjectIDs.LevelCompletedMenu)]
        [SerializeField] private SceneField _levelCompletedMenu = null;

        [SerializeField] private LevelData _currentLevel = null;

        [SerializeField] private LevelsDataSO _levelsData = null;

        [SerializeField] private EventBus _eventBus = null;

        [Inject]
        public LevelManager(LevelsDataSO levelsData, EventBus eventBus)
        {
            _levelsData = levelsData;
            _eventBus = eventBus;

            _eventBus.Subscribe<LevelLoadSignal>(LoadLevel);
            _eventBus.Subscribe<LevelResetSignal>(ResetLevel);
            _eventBus.Subscribe<LevelLoadNextSignal>(LoadNextLevel);

            Application.quitting += Dispose;
        }

        private void LoadNextLevel(LevelLoadNextSignal signal)
        {
            int nextIndex = 1 + _levelsData.GetLevelList().IndexOf(_currentLevel);

            if (nextIndex >= _levelsData.GetLevelList().Count)
            {
                throw new Exception("Next level do not exists");
            }

            LoadLevel(nextIndex);
        }

        private void ResetLevel(LevelResetSignal signal)
        {
            LoadLevel(_currentLevel);
        }

        private void LoadLevel(LevelLoadSignal signal)
        {
            LoadLevel(signal.LevelIndex);
        }

        public IEnumerable<LevelData> GetLevelsData()
        {
            return _levelsData.GetLevelList();
        }

        private void LoadLevel(int levelIndex)
        {
            LoadLevel(_levelsData.GetLevelAt(levelIndex));
        }

        private void LoadLevel(LevelData level)
        {
            _currentLevel = level;
            SceneManager.LoadScene(level.LevelScene);
            SceneManager.LoadScene(_ingameMenuScene, LoadSceneMode.Additive);
            SceneManager.LoadScene(_levelCompletedMenu, LoadSceneMode.Additive);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<LevelLoadSignal>(LoadLevel);
            _eventBus.Unsubscribe<LevelResetSignal>(ResetLevel);
            _eventBus.Unsubscribe<LevelLoadNextSignal>(LoadNextLevel);
        }
    }
}