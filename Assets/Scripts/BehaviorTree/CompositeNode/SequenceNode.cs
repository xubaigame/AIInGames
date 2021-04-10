// ****************************************************
//     文件：SequenceNode.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/4/10 20:40:3
//     功能：顺序节点
// *****************************************************

namespace BehaviorTree
{
    public class SequenceNode : CompositeNode
    {
        private int index;


        public SequenceNode()
        {
            Reset();
        }
        
        public override ResultTypes DoAction()
        {
            if (childrenNodes == null || childrenNodes.Count == 0)
            {
                return ResultTypes.FAIL;
            }

            if (index >= childrenNodes.Count)
            {
                Reset();
            }

            ResultTypes resultType = ResultTypes.NONE;
            for (int length = childrenNodes.Count; index <length ; ++index)
            {
                resultType = childrenNodes[index].DoAction();
                if (resultType == ResultTypes.FAIL)
                {
                    Reset();
                    return resultType;
                }
                else if(resultType==ResultTypes.RUNNING)
                {
                    return resultType;
                }
                else
                {
                    continue;
                }
            }
            
            Reset();
            return ResultTypes.SUCCESSFUL;
        }
        
        private void Reset()
        {
            index = 0;
        }
    }
}

