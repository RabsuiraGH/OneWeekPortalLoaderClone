using System;
using SceneFieldTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core.IngameMenu
{
    public class IngameMenuController : MonoBehaviour
    {
        [SerializeField] private SceneField _startMenuScene;

        [SerializeField] private RectTransform _settingsMenu;

        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _backToStartMenuButton;

        private void Awake()
        {
            _continueButton.onClick.AddListener(HideMenu);
            _backToStartMenuButton.onClick.AddListener(BackToStartMenu);
        }

        private void BackToStartMenu()
        {
            SceneManager.LoadScene(_startMenuScene);
        }

        private void HideMenu()
        {
            _settingsMenu.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _continueButton.onClick.RemoveAllListeners();
            _backToStartMenuButton.onClick.RemoveAllListeners();
        }
    }
}