using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class Player_Move : StateBase
{
    public Player_Controller player;
    private float runTransition = 0;
    private float moveSpeed = 90;
    private float rotateSpeed = 90;

    private bool isRun
    {
        get
        {
            bool temp = player.input.GetRunKey() && player.input.Vertical > 0;
            if (temp) moveSpeed = 200f;
            else moveSpeed = 100f;
            return temp;
        }
    }

    public override void Init(FSMController controller, Enum stateType)
    {
        base.Init(controller, stateType);
        player = controller as Player_Controller;
    }

    public override void OnUpdate()
    {
        float h = player.input.Horizontal;
        float v = player.input.Vertical;

        if(v >= 0)
        {
            if (isRun && runTransition < 1) runTransition += Time.deltaTime;
            else if(!isRun && runTransition > 0) runTransition -= Time.deltaTime;
        }
        else if(runTransition > 0) runTransition -= Time.deltaTime;
        Move(h, v + runTransition);
    }

    // 移动
    private void Move(float h, float v)
    {
        // 移动
        Vector3 dir = new Vector3(0, 0, v);
        // 将方向转变为自身的方向（原先为世界坐标系）
        dir = player.transform.TransformDirection(dir);
        player.characterController.SimpleMove(dir * Time.deltaTime * moveSpeed);

        // 旋转
        Vector3 rot = new Vector3(0, h, 0);
        player.transform.Rotate(rot * Time.deltaTime * rotateSpeed);

        // 同步模型动画
        player.model.UpdateMovePar(h, v);
    }

    public override void OnEnter() { }

    public override void OnExit() { }
}
