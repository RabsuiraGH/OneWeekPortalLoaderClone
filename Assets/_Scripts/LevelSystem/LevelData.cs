using System;
using SceneFieldTools;
using UnityEngine;

namespace Core.Level
{
    [Serializable]
    public class LevelData
    {
        [field: SerializeField] public string _levelName { get; private set; } = string.Empty;

        [field: SerializeField] public SceneField _levelScene { get; private set; } = new();
    }
}