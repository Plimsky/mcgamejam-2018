using System.Runtime.InteropServices;
using UnityEngine;

namespace Utils
{
    public class DestroyEndAnimation : MonoBehaviour
    {
        public Animator Animator;

        void Awake()
        {
            Animator = GetComponent<Animator>();
        }

        void Update()
        {
            AnimatorStateInfo animationState = Animator.GetCurrentAnimatorStateInfo(0);
            AnimatorClipInfo[] myAnimatorClip = Animator.GetCurrentAnimatorClipInfo(0);
            float myTime = myAnimatorClip[0].clip.length * animationState.normalizedTime;
            AnimationClip[] clips = Animator.runtimeAnimatorController.animationClips;

            if (myTime > clips[0].length)
                Destroy(gameObject);
        }
    }
}