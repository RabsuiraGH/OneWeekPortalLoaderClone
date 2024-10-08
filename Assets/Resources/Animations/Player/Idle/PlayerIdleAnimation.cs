using Core.CustomAnimationSystem;
using UnityEngine;

namespace Core
{
    public class PlayerIdleAnimation : CustomAnimationBase
    {
        public override float AnimationSpeed { get; protected set; } = 1;
        public override string AnimationName { get; protected set; } = "Idle";
        protected override string AnimationAdditionalTag { get; set; } = string.Empty;
    }
}
