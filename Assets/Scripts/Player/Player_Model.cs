using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 动画、武器层、刀光效果
public class Player_Model : MonoBehaviour
{
    private Player_Controller player;
    private Animator animator;

    public void Init(Player_Controller player)
    {
        this.player = player;
        animator = GetComponent<Animator>();
    }

    // 更新移动相关参数
    public void UpdateMovePar(float x, float y)
    {
        animator.SetFloat("左右", x);
        animator.SetFloat("前后", y);
    }
}
