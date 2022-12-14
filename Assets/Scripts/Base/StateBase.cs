using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 状态对象基类
/// 之后所有的状态都继承自这个基类
/// 如：Idle Walk Attack
/// </summary>

public abstract class StateBase<T>
{
    // 当前状态对象代表的枚举状态
    public T StateType;

    // 首次实例化时的初始化，比构造函数方便
    public virtual void Init(FSMController<T> controller,T stateType)
    {
        this.StateType = stateType;
    }

    // 进入
    public abstract void OnEnter();

    // 更新
    public abstract void OnUpdate();

    // 退出
    public abstract void OnExit();

}
