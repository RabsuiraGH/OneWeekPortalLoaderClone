using System;
using SceneFieldTools;
using UnityEngine;

namespace Core.Level.Data
{
    [Serializable]
    public class LevelData
    {
#if UNITY_EDITOR
        [SerializeField] private string _levelDevName = string.Empty;
#endif

        [field: SerializeField] public string LevelName { get; private set; } = string.Empty;

        [field: SerializeField] public SceneField LevelScene { get; private set; } = new();
    }
}