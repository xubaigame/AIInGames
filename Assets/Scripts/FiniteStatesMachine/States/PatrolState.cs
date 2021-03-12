/****************************************************
    文件：PatrolState.cs
	作者：积极向上小木木
    邮箱：positivemumu@126.com
    日期：2020/11/10 22:22:46
	功能：Nothing
*****************************************************/

using System.Collections.Generic;
using UnityEngine;

public class PatrolState : FSMBaseState
{
    private Transform[] _paths;
    private Transform _owner;
    private Transform _player;
    private int _pathIndex;
    public PatrolState(FSMStates state,FSMSystem fsmSystem,Transform[] path, Transform owner,Transform player) : base(state,fsmSystem)
    {
        _paths = path;
        _pathIndex = 0;
        _owner = owner;
        _player = player;
    }

    public override void BeforeEnteringState()
    {
        
    }
    public override void Action()
    {
        if(Vector3.Distance(_owner.transform.position, _player.transform.position) <= 3)
        {
            fsmSystem.PerformTransition(FSMTransitions.LookPlayer);
        }
    }

    public override void Reason()
    {
        Vector3 dir = new Vector3(_paths[_pathIndex].position.x, _owner.transform.position.y, _paths[_pathIndex].position.z) - _owner.transform.position;
        if(dir.magnitude<1)
        {
            _pathIndex++;
            if(_pathIndex>=_paths.Length)
            {
                _pathIndex = 0;
            }
        }
        Debug.Log(1);
        _owner.GetComponent<Rigidbody>().velocity = dir.normalized * 5;
        _owner.LookAt(_paths[_pathIndex]);

    }
}