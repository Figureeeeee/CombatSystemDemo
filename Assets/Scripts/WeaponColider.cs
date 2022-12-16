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


    // ��ǰ���ܵ���������
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
        
        // ���enemy�б�
        enemyList.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        // ��֤�˺�ֻ�����һ��enemy
        // �Է���enemy���Ҵ˴ι�����һ��������
        if(other.tag == "Enemy" && !enemyList.Contains(other.gameObject))
        {
            enemyList.Add(other.gameObject);

            // ���˺�ʵ�������enemy������Ӳֱ�����ˡ����ɡ�����ֵ����
            other.GetComponent<Enemy_Controller>().Hurt(hitModel.HardTime, model.transform, hitModel.RepelVelocity, hitModel.RepelTransitionTime, hitModel.DamageValue);

            if (hitModel.SkillHitEF != null) 
            {
                // ��������-���еĵط����� ClosestPointOnBounds����һ�����꣬�����������ʹ���������ײ��
                SpawnObjectByHit(hitModel.SkillHitEF.Spawn, other.ClosestPointOnBounds(transform.position));
                // ������Ч
                if (hitModel.SkillHitEF.AudioClip != null)
                {
                    model.PlayAudio(hitModel.SkillHitEF.AudioClip);
                }
            }
        }
    }

    // �������� ��������
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
