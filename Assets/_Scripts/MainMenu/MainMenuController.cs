using System.Collections.Generic;
using System.Linq;
using Core.EventSystem;
using Core.Level;
using Core.MainMenu.UI;
using UnityEngine;
using Zenject;

namespace Core.MainMenu.Controller
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private StartMenuPageUI _startMenuPage = null;
        [SerializeField] private LevelSelectionPageUI _levelSelectionMenuPage = null;

        [SerializeField] private LevelManager _levelManager = null;

        [SerializeField] private EventBus _eventBus = null;

        [Inject]
        public void Construct(LevelManager levelManager, EventBus eventBus)
        {
            _levelManager = levelManager;
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _eventBus.Subscribe<EscapeCommandSignal>(EscapeButtonLogic);

            PrepareLevels();
            _startMenuPage.OnStartButtonClicked += SwitchToLevelSelectionMenu;
            _startMenuPage.OnExitButtonClicked += ExitGame;

            _levelSelectionMenuPage.OnExitButtonClicked += SwitchToStartMenu;
            _levelSelectionMenuPage.OnLevelSelected += StartLevel;
        }

        private void SwitchToStartMenu()
        {
            _levelSelectionMenuPage.Hide();
            _startMenuPage.Show();
        }

        private void SwitchToLevelSelectionMenu()
        {
            _levelSelectionMenuPage.Show();
            _startMenuPage.Hide();
        }

        public void ExitGame()
        {
#if UNITY_STANDALONE
            Application.Quit();
#endif
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        [ContextMenu(nameof(PrepareLevels))]
        public void PrepareLevels()
        {
            IEnumerable<string> names = _levelManager.GetLevelsData().Select(level => level.LevelName);
            _levelSelectionMenuPage.PrepareUI(names);
        }

        private void StartLevel(int levelIndex)
        {
            _levelManager.LoadLevel(levelIndex);
        }

        public void EscapeButtonLogic(EscapeCommandSignal signal)
        {
            if (_levelSelectionMenuPage.IsOpen())
            {
                SwitchToStartMenu();
            }
            else if (_startMenuPage.IsOpen())
            {
                ExitGame();
            }
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<EscapeCommandSignal>(EscapeButtonLogic);
        }
    }
}