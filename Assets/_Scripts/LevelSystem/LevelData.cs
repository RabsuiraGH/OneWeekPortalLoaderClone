using System;
using SceneFieldTools;
using UnityEngine;

namespace Core.Level
{
    [Serializable]
    public class LevelData
    {
        [field: SerializeField] public string LevelName { get; private set; } = string.Empty;

        [field: SerializeField] public SceneField LevelScene { get; private set; } = new();
    }
}