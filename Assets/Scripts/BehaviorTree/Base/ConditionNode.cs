// ****************************************************
//     文件：ConditionNode.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/4/10 22:5:38
//     功能：条件节点基类
// *****************************************************

namespace BaseBehaviorTree
{
    public class ConditionNode : BaseNode
    {
        public override ResultTypes DoAction()
        {
            return ResultTypes.FAIL;
        }
    }

}
