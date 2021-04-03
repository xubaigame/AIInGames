// ****************************************************
//     文件：HFSMBaseState.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/4/2 20:58:39
//     功能：多层有限状态机状态基类
// *****************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HierarchicalFiniteStatesMachine
{
    public abstract class HFSMBaseState
    {
        protected HFSMBaseSystem hfsmSystem;

        //状态转换字典
        private Dictionary<string, string> transitonToStatesDic;

        //当前状态对应的枚举
        private string name;

        public string Name
        {
            get => name;
        }

        public HFSMBaseState(string name, HFSMBaseSystem hfsmSystem)
        {
            this.name = name;
            this.hfsmSystem = hfsmSystem;
            transitonToStatesDic = new Dictionary<string, string>();
        }

        /// <summary>
        /// 添加一个转换条件
        /// </summary>
        /// <param name="transition">转换条件</param>
        /// <param name="state">要转换到的状态枚举</param>
        public void AddTransition(string transition, string name)
        {
            if (transition == String.Empty||transition == "")
            {
                Debug.Log("HFSMBaseState Error: 转换条件不合法，请尝试更换转换条件！");
                return;
            }

            if (name == String.Empty||name == "")
            {
                Debug.Log("HFSMBaseState Error: 状态名称不合法，请尝试更换状态！");
                return;
            }

            if (transitonToStatesDic.ContainsKey(transition))
            {
                Debug.Log("HFSMBaseState Error: " + this.name + "状态中已经存在转到" + name + "状态的转换条件" +
                          transition.ToString() + "，请勿重复添加！");
                return;
            }

            transitonToStatesDic.Add(transition, name);
        }


        /// <summary>
        /// 移除一个转换条件
        /// </summary>
        /// <param name="transition">转换状态</param>
        public void RemoveAddTransition(string transition)
        {
            if (transition == String.Empty||transition == "")
            {
                Debug.Log("HFSMBaseState Error: 转换条件不合法，请尝试更换转换条件！");
                return;
            }

            if (!transitonToStatesDic.ContainsKey(transition))
            {
                Debug.Log("HFSMBaseState Error: " + name.ToString() + "状态中不存在存在转到其他状态的转换条件" + transition.ToString() +
                          "！");
                return;
            }

            transitonToStatesDic.Remove(transition);
        }


        /// <summary>
        /// 根据转换条件获得要转换的状态
        /// </summary>
        /// <param name="transition">转换条件</param>
        /// <returns></returns>
        public string GetStateByTransition(string transition)
        {
            if (transitonToStatesDic.ContainsKey(transition))
                return transitonToStatesDic[transition];
            return String.Empty;
        }

        /// <summary>
        /// 进入状态之前调用
        /// </summary>
        public virtual void BeforeEnteringState()
        {
        }

        /// <summary>
        /// 切换状态之前调用
        /// </summary>
        public virtual void BeforeLeavingState()
        {
        }

        /// <summary>
        /// 状态切换函数
        /// </summary>
        public abstract void Reason();

        /// <summary>
        /// 执行命令函数
        /// </summary>
        public abstract void Action();
    }
}

