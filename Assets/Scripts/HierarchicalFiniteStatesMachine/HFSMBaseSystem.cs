// ****************************************************
//     文件：HFSMBaseSystem.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/4/2 21:1:20
//     功能：多层有限状态机系统基类
// *****************************************************

using System;
using System.Collections.Generic;
using UnityEngine;

namespace HierarchicalFiniteStatesMachine
{
    public class HFSMBaseSystem
    {
        private string name;

        //状态机管理状态列表
        private List<HFSMBaseState> states;

        //当前状态与上一状态
        private HFSMBaseState currentState;
        private HFSMBaseState proviceState;

        public HFSMManagerSystem managerSystem;
        
        public HFSMBaseState CurrentState
        {
            get => currentState;
            set => currentState = value;
        }

        public HFSMBaseState ProviceState
        {
            get => proviceState;
            set => proviceState = value;
        }
        
        public string Name
        {
            get => name;
        }

        public HFSMBaseSystem(string name,HFSMManagerSystem managerSystem)
        {
            this.name = name;
            this.managerSystem = managerSystem;
            states = new List<HFSMBaseState>();
        }

        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="state">状态类对象</param>
        public void AddState(HFSMBaseState state)
        {
            if (state == null)
            {
                Debug.LogError("HFSMBaseSystem ERROR: 添加状态为空！");
                return;
            }

            if (states.Count == 0)
            {
                states.Add(state);
                currentState = state;
                return;
            }

            foreach (var item in states)
            {
                if (item.Name == state.Name)
                {
                    Debug.LogError("HFSMBaseSystem ERROR: 添加已经添加，请勿重复添加！");
                    return;
                }
            }

            states.Add(state);
        }

        /// <summary>
        /// 删除状态
        /// </summary>
        /// <param name="state">状态类对象</param>
        public void DeleteState(string name)
        {
            if (name == String.Empty||name == "")
            {
                Debug.LogError("HFSMBaseSystem ERROR: 默认状态无法作为实际的状态使用！请尝试更换状态！");
                return;
            }

            foreach (var item in states)
            {
                if (item.Name == name)
                {
                    states.Remove(item);
                    return;
                }
            }

            Debug.LogError("HFSMBaseSystem ERROR: 状态列表中不存在" + name + "状态！");
        }
        

        /// <summary>
        /// 执行状态转换
        /// </summary>
        /// <param name="transition">转换条件</param>
        public void PerformTransition(string transition)
        {
            if (transition == String.Empty||transition == "")
            {
                Debug.LogError("HFSMBaseSystem ERROR: 默认转换条件无法作为实际的转换条件使用，请尝试更换转换条件！");
                return;
            }

            string nextStateName = CurrentState.GetStateByTransition(transition);
            if (nextStateName == String.Empty||nextStateName == "")
            {
                Debug.LogError("HFSMBaseSystem ERROR: " + currentState.Name + "状态在" + transition.ToString() +
                               "条件下转换到的状态是空状态，状态转换失败！");
                return;
            }

            SetStateAsCurrent(nextStateName);
        }

        /// <summary>
        /// 设置状态为当前执行状态
        /// </summary>
        /// <param name="stateName">状态名称</param>
        public void SetStateAsCurrent(string stateName)
        {
            foreach (var item in states)
            {
                if (stateName == item.Name)
                {
                    currentState.BeforeLeavingState();
                    proviceState = currentState;
                    item.BeforeEnteringState();
                    currentState = item;
                    return;
                }
            }
            
            Debug.LogError("HFSMBaseSystem ERROR: 状态列表中不存在" + name + "状态！");
        }
        

        /// <summary>
        /// 撤销状态转换
        /// </summary>
        public void RevokeTransition()
        {
            if (proviceState != null)
            {
                HFSMBaseState temp;
                temp = proviceState;
                currentState.BeforeLeavingState();
                proviceState = currentState;
                temp.BeforeEnteringState();
                currentState = temp;
            }
        }

        /// <summary>
        /// 循环执行当前状态方法
        /// </summary>
        public virtual void UpdateMethod()
        {
            currentState.Reason();
            currentState.Action();
        }
        
    }
}

