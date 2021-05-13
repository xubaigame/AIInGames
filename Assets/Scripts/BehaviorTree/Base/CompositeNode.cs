// ****************************************************
//     文件：CompositeNode.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/4/10 18:50:12
//     功能：组合节点基类
// *****************************************************

using System.Collections.Generic;

namespace BaseBehaviorTree
{
    public class CompositeNode : BaseNode
    {
        protected List<BaseNode> childrenNodes;

        public CompositeNode()
        {
            childrenNodes = new List<BaseNode>();
        }

        public void AddChild(BaseNode node)
        {
            this.childrenNodes.Add(node);
        } 
    }
}
