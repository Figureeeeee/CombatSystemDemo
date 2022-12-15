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
            other.GetComponent<Enemy_Controller>().Hurt();
        }
    }
}
