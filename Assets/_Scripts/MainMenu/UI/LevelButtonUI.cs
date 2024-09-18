using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.MainMenu
{
    public class LevelButtonUI : MonoBehaviour
    {
        public event Action<LevelButtonUI> OnClick;

        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Button _button;

        protected void Awake()
        {
            _button.onClick.AddListener(OnClickInvoke);
        }

        public void PrepareButton(string levelName)
        {
            _levelText.text = levelName;
        }

        public void OnClickInvoke()
        {
            OnClick?.Invoke(this);
        }

        protected void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}