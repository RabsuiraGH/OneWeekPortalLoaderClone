using UnityEngine;
using Core.CustomAnimationSystem;

namespace Core
{
    public class PortalIdleAnimation : CustomAnimationBase
    {
        public override float AnimationSpeed { get; protected set; } = 1f;
        public override string AnimationName { get; protected set; } = "Idle";
        protected override string AnimationAdditionalTag { get; set; } = string.Empty;
    }
}
