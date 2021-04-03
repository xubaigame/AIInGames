// ****************************************************
//     文件：BuyState.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/4/3 9:57:42
//     功能：
// *****************************************************

using System.Collections;
using System.Collections.Generic;
using HierarchicalFiniteStatesMachine;
using UnityEngine;
using UnityEngine.UI;

public class BuyState : HFSMBaseState
{
    private MarketSystem system;
    private Text systemText;
    private Text stateText;

    public BuyState(string name, HFSMBaseSystem hfsmSystem,Text systemText,Text stateText) : base(name, hfsmSystem)
    {
        system = (MarketSystem) hfsmSystem;
        this.systemText = systemText;
        this.stateText = stateText;
    }

    public override void Reason()
    {
        if (system.marketTime > 2)
        {
            system.PerformTransition("Pay");
        }
    }

    public override void Action()
    {
        systemText.text = system.Name;
        stateText.text = Name;
        Debug.Log("购买物品中....");
    }
}
