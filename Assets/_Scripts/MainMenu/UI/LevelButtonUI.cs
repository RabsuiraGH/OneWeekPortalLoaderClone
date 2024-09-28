using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.MainMenu.UI
{
    public class LevelButtonUI : MonoBehaviour
    {
        public event Action<LevelButtonUI> OnClick;

        [SerializeField] private TextMeshProUGUI _levelText = null;
        [SerializeField] private Button _button = null;

        protected void Awake()
        {
            _button.onClick.AddListener(OnClickInvoke);
        }

        public void Select()
        {
            _button.Select();
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