using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    private Enemy_Model model;
    private CharacterController characterController;

    private int hp = 100;

    // �Ƿ��ܻ���
    private bool isHurt;
    // �ܻ�����
    private Vector3 hurtVelocity;
    // �ܻ�����ʱ��
    private float hurtTime;
    // ��ǰʱ��
    private float currHurtTime;

    private void Start()
    {
        model = transform.Find("Model").GetComponent<Enemy_Model>();
        characterController = GetComponent<CharacterController>();
        model.Init();
    }

    private void Update()
    {
        if(isHurt)
        {
            currHurtTime += Time.deltaTime;

            // ��hurtTime��ʱ���ƶ��� |hurtVelocity| �ľ���
            characterController.Move(hurtVelocity * Time.deltaTime / hurtTime);

            if(currHurtTime >= hurtTime) isHurt = false;
        }
        else
        {
            characterController.Move(new Vector3(0, -9f, 0) * Time.deltaTime);
        }
    }

    // ����
    public void Hurt()
    {
        // Ӳֱ�벥�Ŷ���
        model.PlayHurtAnimation();
        CancelInvoke("HurtOver"); // ȡ��֮ǰ���ܻ���ִ���е�Ӳֱ
        Invoke("HurtOver", 3); // 1��Ӳֱ

        // ���ˡ�����
        isHurt = true;
        hurtVelocity = new Vector3(0, 1, 1); // ����һ�ף�����һ��
        hurtTime = 0.2f;
        currHurtTime = 0;

        // ����ֵ����
        hp -= 10;
    }

    private void HurtOver()
    {
        model.StopHurtAnimation();
    }
}
