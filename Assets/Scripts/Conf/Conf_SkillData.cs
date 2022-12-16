using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "��������", menuName = "����/��������/��������")]
public class Conf_SkillData : ScriptableObject
{
    // ��������
    public string Name;

    // �ͷ�����
    public Skill_ReleaseModel ReleaseModel;
    // ��������
    public Skill_HitModel HitModel;
    // ��������
    public Skill_EndModel EndModel;
}

// �����ͷ�
[Serializable]
public class Skill_ReleaseModel
{
    // �������� / ������Ϸ����
    public Skill_SpawnObj SpawnObj;
    // ������Ч
    public AudioClip AudioClip;
    // �ܷ���ת
    public bool CanRotate;
}

// ��������
[Serializable]
public class Skill_HitModel
{
    // �˺���ֵ
    public int DamageValue;
    // Ӳֱʱ��
    public float HardTime;
    // ���ɡ�����
    public Vector3 RepelVelocity;
    // ���ɡ����˵Ĺ���ʱ��
    public float RepelTransitionTime;
    // ����Ч��
    public Conf_SkillHitEF SkillHitEF;
}

// ���ܽ���
[Serializable]
public class Skill_EndModel
{
    // �������� / ������Ϸ����
    public Skill_SpawnObj SpawnObj;
}

// ���ܲ�������
[Serializable]
public class Skill_SpawnObj
{
    // ���ɵ�Ԥ����
    public GameObject Prefab;
    // ���ɵ���Ч
    public AudioClip AudioClip;
    // λ��
    public Vector3 Position;
    // ��ת
    public Vector3  Rotation;
    
}