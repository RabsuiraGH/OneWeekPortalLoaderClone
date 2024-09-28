using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.GameEventSystem;
using Core.GameEventSystem.Signals;
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
        [SerializeField] private GameObject _ingameMenu = null;

        [Inject(Id = Zenject.ZenjectIDs.LevelCompletedMenu)]
        [SerializeField] private GameObject _levelCompletedMenu = null;

        [Inject(Id = Zenject.ZenjectIDs.GameplayInterface)]
        [SerializeField] private GameObject _gameplayInterface = null;

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
            _eventBus.Subscribe<GameOverSignal>(GameOver);

            Application.quitting += Dispose;
        }

        private void GameOver(GameOverSignal signal)
        {
            _eventBus.Invoke(new LevelFailureSignal());
        }

        private void LoadNextLevel(LevelLoadNextSignal signal)
        {
            int nextIndex = 1 + _levelsData.GetLevelList().IndexOf(_currentLevel);

            if (nextIndex >= _levelsData.GetLevelList().Count)
            {
                LoadLevel(nextIndex - 1);
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

        private async void LoadLevel(LevelData level)
        {
            _currentLevel = level;
            await LoadSceneAsync(level.LevelScene);
            GameObject.Instantiate(_ingameMenu);
            GameObject.Instantiate(_levelCompletedMenu);
            GameObject.Instantiate(_gameplayInterface);
        }

        private async Task LoadSceneAsync(string sceneName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncLoad.isDone)
            {
                await Task.Yield();
            }
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<LevelLoadSignal>(LoadLevel);
            _eventBus.Unsubscribe<LevelResetSignal>(ResetLevel);
            _eventBus.Unsubscribe<LevelLoadNextSignal>(LoadNextLevel);
            _eventBus.Unsubscribe<GameOverSignal>(GameOver);
        }
    }
}