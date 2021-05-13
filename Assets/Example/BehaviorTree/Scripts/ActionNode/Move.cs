// ****************************************************
//     文件：Move.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/4/10 22:53:30
//     功能：移动动作节点
// *****************************************************

using BaseBehaviorTree;
using UnityEngine;

public class Move : ActionNode
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
        Debug.Log("Move状态执行结果："+resultType);
        return resultType;
    }
}
