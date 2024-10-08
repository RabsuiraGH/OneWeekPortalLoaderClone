using System;
using System.Collections.Generic;
using ModestTree;

namespace Core.CustomAnimationSystem
{
    [Serializable]
    public abstract class CustomAnimationBase
    {
        public abstract float AnimationSpeed { get; protected set; }

        public abstract string AnimationName { get; protected set; }

        protected abstract string AnimationAdditionalTag { get; set; }

        public string GetTag()
        {
            if (AnimationAdditionalTag == null ||
                AnimationAdditionalTag == string.Empty ||
                AnimationAdditionalTag == "")

                return string.Empty;
            else
                return "_" + AnimationAdditionalTag;
        }

        public void SetTags(params string[] tags)
        {
            string[] currentTags = AnimationAdditionalTag.Split('_');
            List<string> newTag = new();

            for (int i = 0; i < tags.Length; i++)
            {
                if (tags[i] is null && i < currentTags.Length)
                {
                    newTag.Add(currentTags[i]);
                    continue;
                }

                newTag.Add(tags[i]);
            }

            AnimationAdditionalTag = newTag.Join("_");
        }

        public virtual void ChangeAnimationSpeed(float newSpeed)
        {
            AnimationSpeed = newSpeed;
        }
    }
}