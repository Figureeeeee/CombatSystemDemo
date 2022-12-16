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
    // �˺���ֵ
    public int DamageValue;
}

// ��������
[Serializable]
public class Skill_HitModel
{

}

// ���ܽ���
[Serializable]
public class Skill_EndModel
{

}
