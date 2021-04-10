// ****************************************************
//     文件：DecoratorNode.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/4/10 22:0:24
//     功能：装饰节点基类
// *****************************************************

namespace BehaviorTree
{
    public class DecoratorNode : BaseNode
    {
        protected BaseNode child;

        public DecoratorNode()
        {
            child = null;
        }

        protected void SetChild(BaseNode node)
        {
            child = node;
        }
    }
}

