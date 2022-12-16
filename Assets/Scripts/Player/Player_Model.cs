using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 动画、武器层、刀光效果
public class Player_Model : MonoBehaviour
{
    private Player_Controller player;
    private Animator animator;
    public WeaponColider WeaponColider;

    // 当前技能数据
    private Conf_SkillData skillData;

    public void Init(Player_Controller player)
    {
        this.player = player;
        animator = GetComponent<Animator>();
        WeaponColider.Init(this);
    }

    public void PlayAudio(AudioClip audioClip)
    {
        player.PlayAudio(audioClip);
    }

    // 更新移动相关参数
    public void UpdateMovePar(float x, float y)
    {
        animator.SetFloat("左右", x);
        animator.SetFloat("前后", y);
    }

    public void StartAttack(Conf_SkillData conf)
    {
        skillData = conf;
        animator.SetBool("攻击", true);
    }

    private void SpawnObject(Skill_SpawnObj spawn)
    {
        if (spawn != null && spawn.Prefab != null)
        {
            GameObject temp = GameObject.Instantiate(spawn.Prefab.gameObject, null);
            temp.transform.position = transform.position + spawn.Position;
            temp.transform.eulerAngles = player.transform.eulerAngles + spawn.Rotation;
            PlayAudio(spawn.AudioClip);
        }
    }

    #region 动画事件

    // 开始技能伤害
    private void StartSkillHit()
    {
        // 开启刀光的拖尾
        // 开启伤害检测的触发器
        WeaponColider.StartSkillHit(skillData.HitModel);

        // 生成释放时的游戏物体/粒子
        SpawnObject(skillData.ReleaseModel.SpawnObj);
        // 音效
        PlayAudio(skillData.ReleaseModel.AudioClip);
    }

    // 停止技能伤害
    private void StopSkillHit()
    {
        // 关闭刀光的拖尾
        // 关闭伤害检测的触发器
        WeaponColider.StopSkillHit();
    }

    // 技能结束
    private void SkillOver()
    {
        SpawnObject(skillData.EndModel.SpawnObj);

        animator.SetBool("攻击", false);
    }

    #endregion
}
