using UnityEngine;

namespace Extension
{
    public static class AnimatorExtension
    {
        public static AnimationClip FindAnimationClip(this Animator animator, string name) 
        {
            foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == name)
                {
                    return clip;
                }
            }

            return null;
        }
    }
}
