// ****************************************************
//     文件：HomeSystem.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/4/2 22:57:6
//     功能：家中子状态机
// *****************************************************

using System.Collections;
using System.Collections.Generic;
using HierarchicalFiniteStatesMachine;
using UnityEngine;

public class HomeSystem : HFSMBaseSystem
{
    public float homeTime;
    
    public HomeSystem(string name, HFSMManagerSystem managerSystem) : base(name, managerSystem)
    {
        homeTime = 0;
    }
    public override void UpdateMethod()
    {
        homeTime += Time.deltaTime;
        base.UpdateMethod();
    }

    
}
