// ****************************************************
//     文件：GameManager.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：#CreateTime#
//     功能：游戏管理类
// *****************************************************

using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance != null)
                return instance;
            return null;
        }
    }
    
    private List<BehaviorTree> notFlag=new List<BehaviorTree>();

    private List<BehaviorTree> Flag=new List<BehaviorTree>();

    private void Awake()
    {
        instance = this;
        BehaviorTree[] trees = GameObject.FindObjectsOfType<BehaviorTree>();

        foreach (var VARIABLE in trees)
        {
            if (VARIABLE.Group == 1)
                notFlag.Add(VARIABLE);
            else
            {
                Flag.Add(VARIABLE);
            }
        }
    }


    public void ChangeToFlagState()
    {
        foreach (var VARIABLE in notFlag)
        {
            VARIABLE.DisableBehavior();
        }
        foreach (var VARIABLE in Flag)
        {
            VARIABLE.EnableBehavior();
        }
    }

    public void ChangeToNotFlagState()
    {
        foreach (var VARIABLE in notFlag)
        {
            VARIABLE.EnableBehavior();
        }
        foreach (var VARIABLE in Flag)
        {
            VARIABLE.DisableBehavior();
        }
    }

    
}
