/****************************************************
    文件：FollowPlayerState.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/10 22:23:32
	功能：Nothing
*****************************************************/

using UnityEngine;

public class FollowPlayerState : FSMBaseState
{
    public FollowPlayerState(FSMStates state, FSMSystem fsmSystem) : base(state, fsmSystem) { }

    public override void Action(GameObject owner, GameObject player)
    {
        if (Vector3.Distance(owner.transform.position, player.transform.position) >= 15)
        {
            fsmSystem.PerformTransition(FSMTransitions.MissPlayer);
        }
    }

    public override void Reason(GameObject owner, GameObject player)
    {
        Vector3 dir = player.transform.position - owner.transform.position;

        owner.transform.Translate(dir.normalized * 5 * Time.deltaTime);
    }
}