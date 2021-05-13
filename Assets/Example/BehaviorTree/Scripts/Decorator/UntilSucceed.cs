// ****************************************************
//     文件：UntilSucceed.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/4/11 0:6:42
//     功能：
// *****************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using BaseBehaviorTree;
using UnityEngine;

public class UntilSucceed : DecoratorNode
{

    public UntilSucceed(BaseNode node)
    {

        SetChild(node);
    }
    public override ResultTypes DoAction()
    {
        ResultTypes resultType = ResultTypes.NONE;
        do
        {
            resultType=child.DoAction();
        } while (resultType!=ResultTypes.SUCCESSFUL);

        Debug.Log("直到成功装饰节点执行结果为："+resultType);
        return resultType;
    }
}
