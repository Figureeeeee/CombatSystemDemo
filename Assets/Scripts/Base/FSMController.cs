using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 有限状态机控制器
/// 玩家、怪物这样的角色
/// </summary>

public abstract class FSMController<T> : MonoBehaviour
{
    // 当前的状态
    public T CurrentState;

    // 当前的状态对象
    protected StateBase<T> CurrStateObj;

    // 存放全部状态对象（对象池）
    private Dictionary<T, StateBase<T>> stateDic = new Dictionary<T, StateBase<T>>();

    // 修改状态
    public void ChangeState<K>(T newState, bool reCurrState = false) where K : StateBase<T>, new()
    {
        // 如果新状态和当前状态一致，同时并不需要刷新状态
        if (newState.Equals(CurrentState) && !reCurrState) return;

        // 如果当前状态存在，应该执行其退出
        if (CurrStateObj != null) CurrStateObj.OnExit();

        // 基于新状态的枚举获取一个新的状态对象
        CurrStateObj = GetStateObj<K>(newState);
        CurrStateObj.OnEnter();
    }

    // 获取状态对象，输入一个枚举，返回一个与枚举同名的对象
    private StateBase<T> GetStateObj<K>(T stateType) where K : StateBase<T>, new()
    {
        // 如果每次调用这个方法都new新的对象，性能消耗很大，可以定义一个字典存放全部状态对象
        // 遍历这个字典，看一下库存里有没有这个枚举状态
        if (stateDic.ContainsKey(stateType)) return stateDic[stateType];

        // 库存里没有这个枚举状态，那么就实例化一个并且返回
        StateBase<T> state = new K();
        state.Init(this, stateType);
        stateDic.Add(stateType, state);
        return state;
    }

    protected virtual void Update()
    {
        if (CurrStateObj != null) CurrStateObj.OnUpdate();
    }
}
