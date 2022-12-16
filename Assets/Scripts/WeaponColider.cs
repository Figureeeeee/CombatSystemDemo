using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColider : MonoBehaviour
{
    public BoxCollider BoxCollider;
    public TrailRenderer TrailRenderer;

    private List<GameObject> enemyList;

    private Player_Model model;


    // 当前技能的命中数据
    private Skill_HitModel hitModel;

    public void Init(Player_Model model)
    {
        enemyList = new List<GameObject>();
        this.model = model;
        StopSkillHit();
    }

    public void StartSkillHit(Skill_HitModel hitModel)
    {
        this.hitModel = hitModel;
        BoxCollider.enabled = true;
        TrailRenderer.emitting = true;
    }

    public void StopSkillHit()
    {
        BoxCollider.enabled = false;
        TrailRenderer.emitting = false;
        
        // 清空enemy列表
        enemyList.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 保证伤害只会击中一次enemy
        // 对方是enemy并且此次攻击第一次碰到它
        if(other.tag == "Enemy" && !enemyList.Contains(other.gameObject))
        {
            enemyList.Add(other.gameObject);

            // 将伤害实际输出给enemy，附加硬直、击退、击飞、生命值削减
            other.GetComponent<Enemy_Controller>().Hurt(hitModel.HardTime, model.transform, hitModel.RepelVelocity, hitModel.RepelTransitionTime, hitModel.DamageValue);

            if (hitModel.SkillHitEF != null) 
            {
                // 生成粒子-命中的地方坐标 ClosestPointOnBounds：传一个坐标，返回这个坐标和触发器的碰撞点
                SpawnObjectByHit(hitModel.SkillHitEF.Spawn, other.ClosestPointOnBounds(transform.position));
                // 播放音效
                if (hitModel.SkillHitEF.AudioClip != null)
                {
                    model.PlayAudio(hitModel.SkillHitEF.AudioClip);
                }
            }
        }
    }

    // 产生物体 基于命中
    private void SpawnObjectByHit(Skill_SpawnObj spawn, Vector3 spawnPoint)
    {
        if(spawn != null && spawn.Prefab != null)
        {
            GameObject  temp = GameObject.Instantiate(spawn.Prefab.gameObject, null);
            temp.transform.position = spawnPoint + spawn.Position;
            temp.transform.LookAt(Camera.main.transform);
            temp.transform.eulerAngles += spawn.Rotation;
            model.PlayAudio(spawn.AudioClip);
        }
    }
}
