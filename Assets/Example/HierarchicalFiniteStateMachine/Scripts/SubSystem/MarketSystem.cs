// ****************************************************
//     文件：MarketSystem.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/4/3 9:34:22
//     功能：超时子状态机
// *****************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HierarchicalFiniteStatesMachine
{
    public class MarketSystem:HFSMBaseSystem
    {
        
        public float marketTime;

        public MarketSystem(string name, HFSMManagerSystem managerSystem) : base(name, managerSystem)
        {
            marketTime = 0;
        }
        public override void UpdateMethod()
        {
            marketTime += Time.deltaTime;
            base.UpdateMethod();
        }

        
    }
}

