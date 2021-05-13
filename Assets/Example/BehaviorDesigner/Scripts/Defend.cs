// ****************************************************
//     文件：Defend.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：#CreateTime#
//     功能：防御动作拓展
// *****************************************************

using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Movement;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent;
using UnityEngine;
using UnityEngine.AI;

public class Defend : NavMeshMovement
{
    public SharedFloat viewDistance;
    public SharedFloat fieldOfViewAngle;

    public SharedFloat speed;
    public SharedFloat angularSpeed;

    public SharedGameObject target;//要防御的目标 
    public SharedVector3 targetPosition;
    
    private float sqrViewDistance;
    

    public override void OnStart() {
        sqrViewDistance = viewDistance.Value*viewDistance.Value;
    }
    

    //如果抢夺者在视野内，就追， 否则就认为防御成功
    public override TaskStatus OnUpdate() {
        //做一个安全的校验
        if (target == null && target.Value == null) {
            return TaskStatus.Failure;
        }
        float sqrDistance = (target.Value.transform.position - transform.position).sqrMagnitude;
        float angle = Vector3.Angle(transform.forward, target.Value.transform.position - transform.position);
        if (sqrDistance < sqrViewDistance && angle < fieldOfViewAngle.Value*0.5f) { 
            SetDestination(Target()); 
            return TaskStatus.Running;
        }
        else {
            return TaskStatus.Success;
        }
    }
    
    private Vector3 Target()
    {
        if (target.Value != null) {
            return target.Value.transform.position;
        }
        return targetPosition.Value;
    }
}
