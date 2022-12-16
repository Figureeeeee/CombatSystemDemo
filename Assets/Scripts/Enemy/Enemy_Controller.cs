using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    private Enemy_Model model;
    private CharacterController characterController;

    private int hp = 100;

    // 是否受击中
    private bool isRepel;
    // 受击力量
    private Vector3 repelVelocity;
    // 受击过渡时间
    private float repelTime;
    // 当前时间
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

            // 用hurtTime的时间移动了 |hurtVelocity| 的距离
            characterController.Move(repelVelocity * Time.deltaTime / repelTime);

            if(currRepelTime >= repelTime) isRepel = false;
        }
        else
        {
            characterController.Move(new Vector3(0, -9f, 0) * Time.deltaTime);
        }
    }

    // 受伤
    public void Hurt(float hardTime, Transform sourceTranform, Vector3 repelVelocity, float repelTransitionTime, int damageValue)
    {
        // 硬直与播放动画
        model.PlayHurtAnimation();
        // 取消之前可能还在执行中的硬直
        CancelInvoke("HurtOver"); 
        Invoke("HurtOver", hardTime); // 1秒硬直

        // 击退、击飞
        isRepel = true;
        this.repelVelocity = sourceTranform.TransformDirection(repelVelocity); // 击飞一米，击退一米
        repelTime = repelTransitionTime;
        currRepelTime = 0;

        // 生命值减少
        hp -= damageValue;
    }

    private void HurtOver()
    {
        model.StopHurtAnimation();
    }
}
