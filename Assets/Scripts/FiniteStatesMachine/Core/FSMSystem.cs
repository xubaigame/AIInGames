/****************************************************
    文件：FSMSystem.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/10 22:3:32
	功能：有限状态机管理类
*****************************************************/

using System.Collections.Generic;
using UnityEngine;

public class FSMSystem
{
    //状态机管理状态列表
    private List<FSMBaseState> _states;

    //当前状态与上一状态
    private FSMBaseState _currentState;
    private FSMBaseState _proviceState;
    public FSMBaseState CurrentState { get => _currentState; set => _currentState = value; }
    public FSMBaseState ProviceState { get => _proviceState; set => _proviceState = value; }

    public FSMSystem()
    {
        _states = new List<FSMBaseState>();
    }

    /// <summary>
    /// 添加状态
    /// </summary>
    /// <param name="state">状态类对象</param>
    public void AddState(FSMBaseState state)
    {
        if (state == null)
        {
            Debug.LogError("FSMSystem ERROR: 添加状态为空！");
            return;
        }

        if (_states.Count == 0)
        {
            _states.Add(state);
            _currentState = state;
            return;
        }
        foreach (var item in _states)
        {
            if (item.State == state.State)
            {
                Debug.LogError("FSMSystem ERROR: 添加已经添加，请勿重复添加！");
                return;
            }
        }
        _states.Add(state);
    }

    /// <summary>
    /// 删除状态
    /// </summary>
    /// <param name="state">状态类对象</param>
    public void DeleteState(FSMStates state)
    {
        if (state == FSMStates.NullState)
        {
            Debug.LogError("FSMSystem ERROR: 默认状态无法作为实际的状态使用！请尝试更换状态！");
            return;
        }

        foreach (var item in _states)
        {
            if (item.State == state)
            {
                _states.Remove(item);
                return;
            }
        }
        Debug.LogError("FSMSystem ERROR: 状态列表中不存在" + state.ToString() + "状态！");
    }

    /// <summary>
    /// 执行状态转换
    /// </summary>
    /// <param name="transition">转换条件</param>
    public void PerformTransition(FSMTransitions transition)
    {
        if (transition == FSMTransitions.NullTransition)
        {
            Debug.LogError("FSMSystem ERROR: 默认转换条件无法作为实际的转换条件使用，请尝试更换转换条件！");
            return;
        }
        FSMStates nextState = CurrentState.GetStateByTransition(transition);
        if (nextState == FSMStates.NullState)
        {
            Debug.LogError("FSMSystem ERROR: " + _currentState.State.ToString() + "状态在" + transition.ToString() + "条件下转换到的状态是空状态，状态转换失败！");
            return;
        }

        foreach (var item in _states)
        {
            if (nextState == item.State)
            {
                _currentState.BeforLeavingState();
                _proviceState = _currentState;
                item.BeforeEnteringState();
                _currentState = item;
                break;
            }
        }
    }

    /// <summary>
    /// 撤销状态转换
    /// </summary>
    public void RevokeTransition()
    {
        if (_proviceState != null)
        {
            FSMBaseState temp;
            temp = _proviceState;
            _currentState.BeforLeavingState();
            _proviceState = _currentState;
            temp.BeforeEnteringState();
            _currentState = temp;
        }
    }

}