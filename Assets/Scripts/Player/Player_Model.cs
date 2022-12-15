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

    public void StartAttack()
    {
        animator.SetBool("攻击", true);
    }

    #region 动画事件

    // 开始技能伤害
    private void StartSkillHit()
    {
        // 开启刀光的拖尾
        // 开启伤害检测的触发器
    }

    // 停止技能伤害
    private void StopSkillHit()
    {
        // 关闭刀光的拖尾
        // 关闭伤害检测的触发器
    }

    // 技能结束
    private void SkillOver()
    {
        animator.SetBool("攻击", false);
    }

    #endregion
}
