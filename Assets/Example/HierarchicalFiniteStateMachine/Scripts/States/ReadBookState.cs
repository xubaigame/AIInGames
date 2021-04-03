// ****************************************************
//     文件：ReadBookState.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/4/2 22:55:0
//     功能：家中子状态机管理的读书状态类
// *****************************************************

using HierarchicalFiniteStatesMachine;
using UnityEngine;
using UnityEngine.UI;

public class ReadBookState : HFSMBaseState
{
    private HomeSystem system;
    private Text systemText;
    private Text stateText;
    public ReadBookState(string name, HFSMBaseSystem hfsmSystem,Text systemText,Text stateText) : base(name, hfsmSystem)
    {
        system = (HomeSystem) hfsmSystem;
        this.systemText = systemText;
        this.stateText = stateText;
    }

    public override void Reason()
    {
        if (system.homeTime > 2)
        {
            system.PerformTransition("Cook");
        }
    }

    public override void Action()
    {
        systemText.text = system.Name;
        stateText.text = Name;
        Debug.Log("读书中....");
    }
}
