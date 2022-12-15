using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    private Enemy_Model model;
    private CharacterController characterController;

    private int hp = 100;

    // 是否受击中
    private bool isHurt;
    // 受击力量
    private Vector3 hurtVelocity;
    // 受击过渡时间
    private float hurtTime;
    // 当前时间
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

            // 用hurtTime的时间移动了 |hurtVelocity| 的距离
            characterController.Move(hurtVelocity * Time.deltaTime / hurtTime);

            if(currHurtTime >= hurtTime) isHurt = false;
        }
        else
        {
            characterController.Move(new Vector3(0, -9f, 0) * Time.deltaTime);
        }
    }

    // 受伤
    public void Hurt()
    {
        // 硬直与播放动画
        model.PlayHurtAnimation();
        CancelInvoke("HurtOver"); // 取消之前可能还在执行中的硬直
        Invoke("HurtOver", 3); // 1秒硬直

        // 击退、击飞
        isHurt = true;
        hurtVelocity = new Vector3(0, 1, 1); // 击飞一米，击退一米
        hurtTime = 0.2f;
        currHurtTime = 0;

        // 生命值减少
        hp -= 10;
    }

    private void HurtOver()
    {
        model.StopHurtAnimation();
    }
}
