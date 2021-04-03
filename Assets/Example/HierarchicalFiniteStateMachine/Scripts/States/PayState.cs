// ****************************************************
//     文件：PayState.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/4/3 10:0:50
//     功能：
// *****************************************************

using System.Collections;
using System.Collections.Generic;
using HierarchicalFiniteStatesMachine;
using UnityEngine;
using UnityEngine.UI;

public class PayState : HFSMBaseState
{
    private MarketSystem system;
    private Text systemText;
    private Text stateText;

    public PayState(string name, HFSMBaseSystem hfsmSystem,Text systemText,Text stateText) : base(name, hfsmSystem)
    {
        system = (MarketSystem) hfsmSystem;
        this.systemText = systemText;
        this.stateText = stateText;
    }

    public override void Reason()
    {
        if (system.marketTime > 5)
        {
            GameObject.FindObjectOfType<UIController>().number += Random.Range(5,13);
            system.managerSystem.QuitCurrentState();
            Debug.Log("付款完成");
            system.marketTime = 0;
        }
    }

    public override void Action()
    {
        systemText.text = system.Name;
        stateText.text = Name;
        Debug.Log("付款中....");
    }
}
