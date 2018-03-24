using UnityEngine;

namespace Utils
{
    public class RandomOffsetAnimation : MonoBehaviour
    {
        void Awake()
        {
            Animator anim = GetComponent<Animator> ();
            AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo (0);
            anim.Play (state.fullPathHash, -1, Random.Range(0f,4f));
        }
    }
}