// ****************************************************
//     文件：ParallelSelectNode.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/4/10 20:45:52
//     功能：并行选择节点
// *****************************************************

using System.Collections.Generic;
using UnityEngine;

namespace BaseBehaviorTree
{
    public class ParallelSelectNode : CompositeNode
    {
        private List<BaseNode> waitNodes;
        private bool isFail;

        public ParallelSelectNode()
        {
            waitNodes = new List<BaseNode>();
            isFail = false;
        }

        public override ResultTypes DoAction()
        {
            if (childrenNodes == null || childrenNodes.Count == 0)
            {
                return ResultTypes.FAIL;
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
                        break;
                    case ResultTypes.RUNNING:
                        tempWaitNodes.Add(tempMainNodes[i]);
                        break;
                    default:
                        isFail = true;
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
            return isFail ? ResultTypes.FAIL : ResultTypes.SUCCESSFUL;
        }

        private void Reset()
        {
            waitNodes.Clear();
            isFail = false;
        }
    }
}

