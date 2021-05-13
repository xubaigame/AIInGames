// ****************************************************
//     文件：BaseNode.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/4/10 18:40:48
//     功能：行为树节点父类
// *****************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseBehaviorTree
{
    public class BaseNode
    {
        public virtual ResultTypes DoAction()
        {
            return ResultTypes.NONE;
        }
    }
}