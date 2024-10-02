namespace Core.CustomAnimationSystem
{
    public interface ICustomAnimator
    {
        public abstract bool IsAnimationPlaying();

        public abstract void PlayAnimation(CustomAnimationBase animationAction);

        public void SetFloat(string parameter, float value);

        public void SetBool(string parameter, bool value);

        public void SetInt(string parameter, int value);
    }
}