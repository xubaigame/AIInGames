// ****************************************************
//     文件：HFSMManagerSystem.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/4/2 21:12:28
//     功能：
// *****************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HierarchicalFiniteStatesMachine
{
    public class HFSMManagerSystem
    {
        private Dictionary<string,HFSMBaseSystem> systems;
        private Stack<HFSMBaseSystem> activeSystems;

        private HFSMBaseSystem currentSystem;
        public HFSMManagerSystem()
        {
            systems = new Dictionary<string, HFSMBaseSystem>();
            activeSystems = new Stack<HFSMBaseSystem>();
        }

        /// <summary>
        /// 添加管理的子状态机
        /// </summary>
        /// <param name="hfsmSystem">子状态机对象</param>
        public void AddSubSystem(HFSMBaseSystem hfsmSystem)
        {
            if (systems.ContainsKey(hfsmSystem.Name))
            {
                Debug.LogError("HFSMManagerSystem ERROR: "+hfsmSystem.Name+"子状态机已经添加，请勿重复添加！");
                return;
            }
            
            if (systems.Count == 0)
            {
                currentSystem = hfsmSystem;
                activeSystems.Push(currentSystem);
            }
            systems.Add(hfsmSystem.Name,hfsmSystem);
        }
        
        /// <summary>
        /// 移除管理的子状态机
        /// </summary>
        /// <param name="hfsmSystem">子状态机的名称</param>
        public void RemoveSubSystem(HFSMBaseSystem hfsmSystem)
        {
            if (!systems.ContainsValue(hfsmSystem))
            {
                Debug.LogError("HFSMManagerSystem ERROR: "+hfsmSystem.Name+"子状态机未被管理状态机管理，删除失败！");
                return;
            }
            systems.Remove(hfsmSystem.Name);
        }

        /// <summary>
        /// 切换当前正在执行的子状态机
        /// </summary>
        /// <param name="hfsmSystemName">子状态机名称</param>
        /// <param name="transition">切换后子状态机要转换的条件</param>
        public void ChangeSystem(string hfsmSystemName,string currentState="")
        {
            if (systems.ContainsKey(hfsmSystemName))
            {
                //proviceSystem = currentSystem;
                currentSystem = systems[hfsmSystemName];
                activeSystems.Push(currentSystem);
                if (currentState != "")
                {
                    currentSystem.SetStateAsCurrent(currentState);
                }
            }
            else
            {
                Debug.LogError("HFSMManagerSystem ERROR: "+hfsmSystemName+"子状态机未被管理状态机管理，删除失败！");
            }
        }

        /// <summary>
        /// 退出当前子状态机，继续执行上一状态。
        /// </summary>
        /// <param name="transition"></param>
        public void QuitCurrentState(string currentState="")
        {
            
            if (activeSystems.Count > 1)
            {
                activeSystems.Pop();
                currentSystem = activeSystems.Peek();
                if (currentState != "")
                {
                    currentSystem.SetStateAsCurrent(currentState);
                }
            }
        }
        
        /// <summary>
        /// 循环执行当前状态方法
        /// </summary>
        public void UpdateMethod()
        {
            currentSystem.UpdateMethod();
        }
        
    }
}


