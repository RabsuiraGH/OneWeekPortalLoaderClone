using System.Collections.Generic;
using System.Linq;
using Core.Level;
using Core.MainMenu.UI;
using UnityEngine;
using Zenject;

namespace Core.MainMenu.Controller
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private StartMenuPageUI _startMenuPage;
        [SerializeField] private LevelSelectionPageUI _levelSelectionMenuPage;

        [SerializeField] private LevelManager _levelManager;

        [Inject]
        public void Construct(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }

        private void Awake()
        {
            PrepareLevels();
            _startMenuPage.OnStartButtonClicked += SwitchToLevelSelectionMenu;
            _startMenuPage.OnExitButtonClicked += ExitGame;

            _levelSelectionMenuPage.OnExitButtonClicked += SwitchToStartMenu;
            _levelSelectionMenuPage.OnLevelSelected += StartLevel;
        }

        [ContextMenu(nameof(PrepareLevels))]
        public void PrepareLevels()
        {
            IEnumerable<string> names = _levelManager.GetLevelsData().Select(level => level.LevelName);
            _levelSelectionMenuPage.PrepareUI(names);
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

        private void SwitchToLevelSelectionMenu()
        {
            _levelSelectionMenuPage.Show();
            _startMenuPage.Hide();
        }

        private void SwitchToStartMenu()
        {
            _levelSelectionMenuPage.Hide();
            _startMenuPage.Show();
        }

        private void StartLevel(int levelIndex)
        {
            _levelManager.LoadLevel(levelIndex);
        }
    }
}