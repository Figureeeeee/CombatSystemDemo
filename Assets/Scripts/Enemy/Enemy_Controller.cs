using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    private Enemy_Model model;
    private CharacterController characterController;

    private int hp = 100;

    // �Ƿ��ܻ���
    private bool isRepel;
    // �ܻ�����
    private Vector3 repelVelocity;
    // �ܻ�����ʱ��
    private float repelTime;
    // ��ǰʱ��
    private float currRepelTime;

    private void Start()
    {
        model = transform.Find("Model").GetComponent<Enemy_Model>();
        characterController = GetComponent<CharacterController>();
        model.Init();
    }

    private void Update()
    {
        if(isRepel)
        {
            currRepelTime += Time.deltaTime;

            // ��hurtTime��ʱ���ƶ��� |hurtVelocity| �ľ���
            characterController.Move(repelVelocity * Time.deltaTime / repelTime);

            if(currRepelTime >= repelTime) isRepel = false;
        }
        else
        {
            characterController.Move(new Vector3(0, -9f, 0) * Time.deltaTime);
        }
    }

    // ����
    public void Hurt(float hardTime, Transform sourceTranform, Vector3 repelVelocity, float repelTransitionTime, int damageValue)
    {
        // Ӳֱ�벥�Ŷ���
        model.PlayHurtAnimation();
        // ȡ��֮ǰ���ܻ���ִ���е�Ӳֱ
        CancelInvoke("HurtOver"); 
        Invoke("HurtOver", hardTime); // 1��Ӳֱ

        // ���ˡ�����
        isRepel = true;
        this.repelVelocity = sourceTranform.TransformDirection(repelVelocity); // ����һ�ף�����һ��
        repelTime = repelTransitionTime;
        currRepelTime = 0;

        // ����ֵ����
        hp -= damageValue;
    }

    private void HurtOver()
    {
        model.StopHurtAnimation();
    }
}
