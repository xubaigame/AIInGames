// ****************************************************
//     文件：IsHasFlag.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：#CreateTime#
//     功能：行为树插件拓展
// *****************************************************

using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class IsHasFlag : Conditional
{
    public Offense offense;

    public override void OnAwake()
    {
        offense = GetComponent<Offense>();
    }

    public override TaskStatus OnUpdate()
    {
        if (offense.HasFlag)
            return TaskStatus.Success;
        return TaskStatus.Failure;
    }
}
