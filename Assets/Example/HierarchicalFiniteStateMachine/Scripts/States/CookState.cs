// ****************************************************
//     文件：CookState.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/4/2 23:6:13
//     功能：家中子状态机管理的做饭状态类
// *****************************************************

using System.Collections;
using System.Collections.Generic;
using HierarchicalFiniteStatesMachine;
using UnityEngine;
using UnityEngine.UI;

public class CookState : HFSMBaseState
{
    private HomeSystem system;
    private Text systemText;
    private Text stateText;
    public CookState(string name, HFSMBaseSystem hfsmSystem,Text systemText,Text stateText) : base(name, hfsmSystem)
    {
        system = (HomeSystem) hfsmSystem;
        this.systemText = systemText;
        this.stateText = stateText;
    }

    public override void Reason()
    {
        if (GameObject.FindObjectOfType<UIController>().number < 4)
        {
            Debug.Log("没盐了，准备去买盐");
            system.managerSystem.ChangeSystem("MarketSystem","BuyState");
        }
        else if (system.homeTime > 5)
        {
            GameObject.FindObjectOfType<UIController>().number -= 4;
            system.PerformTransition("Sleep");
        }
    }

    public override void Action()
    {
        systemText.text = system.Name;
        stateText.text = Name;
        Debug.Log("做饭中....");
    }
}
