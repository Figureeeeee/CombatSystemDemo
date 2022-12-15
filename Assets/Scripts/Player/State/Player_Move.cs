using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class Player_Move : StateBase<PlayerState>
{
    public Player_Controller player;
    private float runTransition = 0;
    private float moveSpeed = 3f;
    private float rotateSpeed = 90f;

    private bool isRun
    {
        get
        {
            bool temp = player.input.GetRunKey() && player.input.Vertical > 0;
            if (temp) moveSpeed = 5f;
            else moveSpeed = 3f;
            return temp;
        }
    }

    public override void Init(FSMController<PlayerState> controller, PlayerState stateType)
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


        // 检测攻击，如果玩家按键攻击则切换到攻击状态
        // 还需要考虑cd等因素
        if (player.CheckAttack()) player.ChangeState<Player_Attack>(PlayerState.Player_Attack);
    }

    // 移动
    private void Move(float h, float v)
    {
        //Debug.Log("moveSpeed:" + moveSpeed);

        // 移动
        Vector3 dir = new Vector3(0, 0, v);
        // 将方向转变为自身的方向（原先为世界坐标系）
        dir = player.transform.TransformDirection(dir);
        player.characterController.SimpleMove(dir * moveSpeed);

        // 旋转
        Vector3 rot = new Vector3(0, h, 0);
        player.transform.Rotate(rot * Time.deltaTime * rotateSpeed);

        // 同步模型动画
        player.model.UpdateMovePar(h, v);
    }

    public override void OnEnter() { }

    public override void OnExit() { }
}
