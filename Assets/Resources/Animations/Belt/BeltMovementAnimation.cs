using System;
using Core.CustomAnimationSystem;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class BeltMovementAnimation : CustomAnimationBase
    {
        [field: SerializeField] public override float AnimationSpeed { get; protected set; } = 1;

        [field: SerializeField] public override string AnimationName { get; protected set; } = "Movement";

        [field: SerializeField] protected override string AnimationAdditionalTag { get; set; } = "";
    }
}