/****************************************************
    文件：FSMBaseState.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/10 22:2:53
	功能：有限状态机状态基类
*****************************************************/
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMBaseState
{
    protected FSMSystem fsmSystem;
    //状态转换字典
    private Dictionary<FSMTransitions, FSMStates> _transitonToStatesDic;
    //当前状态对应的枚举
    private FSMStates _state;
    public FSMStates State { get => _state; set => _state = value; }

    public FSMBaseState(FSMStates state,FSMSystem fsmSystem)
    {
        _state = state;
        this.fsmSystem = fsmSystem;
        _transitonToStatesDic = new Dictionary<FSMTransitions, FSMStates>();
    }

    /// <summary>
    /// 添加一个转换条件
    /// </summary>
    /// <param name="transition">转换条件</param>
    /// <param name="state">要转换到的状态枚举</param>
    public void AddTransition(FSMTransitions transition, FSMStates state)
    {
        if (transition == FSMTransitions.NullTransition)
        {
            Debug.Log("FSMBaseState Error: 默认转换条件无法作为实际的转换条件使用，请尝试更换转换条件！");
            return;
        }

        if (state == FSMStates.NullState)
        {
            Debug.Log("FSMBaseState Error: 默认状态无法作为实际的状态使用，请尝试更换状态！");
            return;
        }

        if (_transitonToStatesDic.ContainsKey(transition))
        {
            Debug.Log("FSMBaseState Error: " + _state.ToString() + "状态中已经存在转到" + state.ToString() + "状态的转换条件" + transition.ToString() + "，请勿重复添加！");
            return;
        }

        _transitonToStatesDic.Add(transition, state);
    }


    /// <summary>
    /// 移除一个转换条件
    /// </summary>
    /// <param name="transition">转换状态</param>
    public void RemoveAddTransition(FSMTransitions transition)
    {
        if (transition == FSMTransitions.NullTransition)
        {
            Debug.Log("FSMBaseState Error: 默认转换条件无法作为实际的转换条件使用，请尝试更换转换条件！");
            return;
        }

        if (!_transitonToStatesDic.ContainsKey(transition))
        {
            Debug.Log("FSMBaseState Error: " + _state.ToString() + "状态中不存在存在转到其他状态的转换条件" + transition.ToString() + "！");
            return;
        }

        _transitonToStatesDic.Remove(transition);
    }


    /// <summary>
    /// 根据转换条件获得要转换的状态
    /// </summary>
    /// <param name="transition">转换条件</param>
    /// <returns></returns>
    public FSMStates GetStateByTransition(FSMTransitions transition)
    {
        if (_transitonToStatesDic.ContainsKey(transition))
            return _transitonToStatesDic[transition];
        return FSMStates.NullState;
    }

    /// <summary>
    /// 进入状态之前调用
    /// </summary>
    public virtual void BeforeEnteringState() { }

    /// <summary>
    /// 切换状态之前调用
    /// </summary>
    public virtual void BeforLeavingState() { }

    /// <summary>
    /// 状态切换函数
    /// </summary>
    public abstract void Reason();

    /// <summary>
    /// 执行命令函数
    /// </summary>
    public abstract void Action();

}