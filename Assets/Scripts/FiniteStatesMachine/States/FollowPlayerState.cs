/****************************************************
    文件：FollowPlayerState.cs
	作者：积极向上小木木
    邮箱：positivemumu@126.com
    日期：2020/11/10 22:23:32
	功能：Nothing
*****************************************************/

using UnityEngine;

public class FollowPlayerState : FSMBaseState
{
    private Transform _owner;
    private Transform _player;
    public FollowPlayerState(FSMStates state, FSMSystem fsmSystem,Transform owner,Transform player) : base(state, fsmSystem) 
    {
        _owner = owner;
        _player = player;
    }

    public override void Action()
    {
        if (Vector3.Distance(_owner.transform.position, _player.transform.position) >= 15)
        {
            fsmSystem.PerformTransition(FSMTransitions.MissPlayer);
        }
    }

    public override void Reason()
    {
        if (Vector3.Distance(_player.transform.position, _owner.transform.position)<0.2f)
        {
            return;
        }
        Vector3 dir = _player.transform.position - _owner.transform.position;

        
        _owner.transform.LookAt(_player);
        _owner.GetComponent<Rigidbody>().velocity = dir.normalized * 5;
        
        //Quaternion lookRot = Quaternion.LookRotation(_player.position);
        //_owner.transform.Translate(dir.normalized * 5 * Time.deltaTime);
        //_owner.rotation = Quaternion.Slerp(_owner.rotation, lookRot, Mathf.Clamp01(5 * Time.deltaTime));
    }
}