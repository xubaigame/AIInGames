// ****************************************************
//     文件：SleepState.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/4/2 23:9:34
//     功能：家中子状态机管理的睡觉状态类
// *****************************************************

using System.Collections;
using System.Collections.Generic;
using HierarchicalFiniteStatesMachine;
using UnityEngine;
using UnityEngine.UI;

public class SleepState : HFSMBaseState
{
    private HomeSystem system;
    private Text systemText;
    private Text stateText;

    public SleepState(string name, HFSMBaseSystem hfsmSystem,Text systemText,Text stateText) : base(name, hfsmSystem)
    {
        system = (HomeSystem) hfsmSystem;
        this.systemText = systemText;
        this.stateText = stateText;
    }

    public override void Reason()
    {
        if (system.homeTime > 10)
        {
            Debug.Log("天亮了");
            system.PerformTransition("ReadBook");
            system.homeTime = 0;
        }
    }

    public override void Action()
    {
        systemText.text = system.Name;
        stateText.text = Name;
        Debug.Log("睡觉中....");
    }
}
