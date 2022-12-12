using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 有限状态机控制器
/// 玩家、怪物这样的角色
/// </summary>

public abstract class FSMController : MonoBehaviour
{
    // 当前的状态
    public abstract Enum CurrentState { get; set; }

    // 当前的状态对象
    protected StateBase CurrStateObj;

    // 存放全部状态对象（对象池）
    private List<StateBase> stateList = new List<StateBase>();

    // 修改状态
    public void ChangeState(Enum newState, bool reCurrState = false)
    {
        // 如果新状态和当前状态一致，同时并不需要刷新状态
        if (CurrentState == newState && !reCurrState) return;

        // 如果当前状态存在，应该执行其退出
        if (CurrStateObj != null) CurrStateObj.OnExit();

        // 基于新状态的枚举获取一个新的状态对象
        CurrStateObj = GetStateObj(newState);
        CurrStateObj.OnEnter();
    }

    // 获取状态对象，输入一个枚举，返回一个与枚举同名的对象
    private StateBase GetStateObj(Enum stateType)
    {
        // 如果每次调用这个方法都new新的对象，性能消耗很大，可以定义一个列表存放全部状态对象
        // 遍历这个列表，看一下库存里有没有这个枚举状态
        for(int i = 0; i < stateList.Count; i ++)
        {
            if (stateList[i].StateType == stateType) return stateList[i];
        }

        // 库存里没有这个枚举状态，那么就实例化一个并且返回
        // 反射
        StateBase state = Activator.CreateInstance(Type.GetType(stateType.ToString())) as StateBase;
        state.Init(this, stateType);
        stateList.Add(state);
        return state;
    }

    protected virtual void Update()
    {
        if (CurrStateObj != null) CurrStateObj.OnUpdate();
    }
}
