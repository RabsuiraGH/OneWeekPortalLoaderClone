using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.StartMenu
{
    public class LevelButtonUI : Button
    {
        public event Action<LevelButtonUI> OnClick;

        protected override void Awake()
        {
            onClick.AddListener(OnClickInvoke);
        }

        public void OnClickInvoke()
        {
            OnClick?.Invoke(this);
        }

        protected override void OnDestroy()
        {
            onClick.RemoveAllListeners();
        }
    }
}