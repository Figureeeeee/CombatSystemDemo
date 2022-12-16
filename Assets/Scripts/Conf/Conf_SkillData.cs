using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "技能配置", menuName = "配置/技能配置/技能数据")]
public class Conf_SkillData : ScriptableObject
{
    // 技能名称
    public string Name;

    // 释放数据
    public Skill_ReleaseModel ReleaseModel;
    // 命中数据
    public Skill_HitModel HitModel;
    // 结束数据
    public Skill_EndModel EndModel;
}

// 技能释放
[Serializable]
public class Skill_ReleaseModel
{
    
}

// 技能命中
[Serializable]
public class Skill_HitModel
{
    // 伤害数值
    public int DamageValue;
    // 硬直时间
    public float HardTime;
    // 击飞、击退
    public Vector3 RepelVelocity;
    // 击飞、击退的过度时间
    public float RepelTransitionTime;
    // 命中效果
    public Conf_SkillHitEF SkillHitEF;
}

// 技能结束
[Serializable]
public class Skill_EndModel
{

}

// 技能产生物体
[Serializable]
public class Skill_SpawnObj
{
    // 生成的预制体
    public GameObject Prefab;
    // 生成的音效
    public AudioClip AudioClip;
    // 位置
    public Vector3 Position;
    // 旋转
    public Vector3  Rotation;
    
}