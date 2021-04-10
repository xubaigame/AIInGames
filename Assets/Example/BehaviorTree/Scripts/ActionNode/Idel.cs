// ****************************************************
//     文件：Idel.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/4/10 23:0:10
//     功能：静止动作节点
// *****************************************************

using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class Idel : ActionNode
{
    public override ResultTypes DoAction()
    {
        ResultTypes resultType = ResultTypes.NONE;
        if (Random.value > 0.5f)
        {
            resultType = ResultTypes.SUCCESSFUL;
        }
        else
        {
            resultType = ResultTypes.FAIL;
        }
        Debug.Log("Idel状态执行结果："+resultType);
        return resultType;
    }
}
