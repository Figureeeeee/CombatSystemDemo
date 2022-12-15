using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Model : MonoBehaviour
{
    private Animator animator;
    private int currHurtAnimationIndex = 1;
    public void Init()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayHurtAnimation()
    {
        animator.SetTrigger(" ‹…À" + currHurtAnimationIndex);
        currHurtAnimationIndex = currHurtAnimationIndex == 1 ? 2 : 1;
    }

    public void StopHurtAnimation()
    {
        animator.SetTrigger(" ‹…ÀΩ· ¯");
    }
}
