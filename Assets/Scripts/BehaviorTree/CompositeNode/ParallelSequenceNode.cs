// ****************************************************
//     文件：ParallelSequenceNode.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/4/10 21:47:12
//     功能：并行顺序节点
// *****************************************************

using System.Collections.Generic;

namespace BehaviorTree
{
    public class ParallelSequenceNode : CompositeNode
    {
        private List<BaseNode> waitNodes;
        private bool isSuccess;
        
        public ParallelSequenceNode()
        {
            waitNodes = new List<BaseNode>();
            isSuccess = false;
        }
        
        public override ResultTypes DoAction()
        {
            if (childrenNodes == null || childrenNodes.Count == 0)
            {
                return ResultTypes.SUCCESSFUL;
            }
            
            ResultTypes resultType = ResultTypes.NONE;
            List<BaseNode> tempWaitNodes = new List<BaseNode>();
            List<BaseNode> tempMainNodes = new List<BaseNode>();

            tempMainNodes = waitNodes.Count > 0 ? waitNodes : childrenNodes;
            for (int i = 0,length=tempMainNodes.Count; i < length; ++i)
            {
                resultType = tempMainNodes[i].DoAction();
                switch (resultType)
                {
                    case ResultTypes.SUCCESSFUL:
                        isSuccess = true;
                        break;
                    case ResultTypes.RUNNING:
                        tempWaitNodes.Add(tempMainNodes[i]);
                        break;
                    default:
                        break;
                }
            }

            if (tempWaitNodes.Count > 0)
            {
                waitNodes = tempWaitNodes;
                return ResultTypes.RUNNING;
            }

            resultType = CheckResult();
            Reset();
            return resultType;
        }
        
        
        private ResultTypes CheckResult()
        {
            return isSuccess ? ResultTypes.SUCCESSFUL : ResultTypes.FAIL;
        }

        private void Reset()
        {
            waitNodes.Clear();
            isSuccess = false;
        }
    }
}