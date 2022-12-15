using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColider : MonoBehaviour
{
    public BoxCollider BoxCollider;
    public TrailRenderer TrailRenderer;

    private List<GameObject> enemyList;
    
    public void Init()
    {
        enemyList = new List<GameObject>();
        StopSkillHit();
    }

    public void StartSkillHit()
    {
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

            // TODO:将伤害实际输出给enemy，附加硬直、击退、击飞、生命值削减
        }
    }
}
