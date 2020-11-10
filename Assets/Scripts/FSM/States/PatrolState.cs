/****************************************************
    文件：PatrolState.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/10 22:22:46
	功能：Nothing
*****************************************************/

using System.Collections.Generic;
using UnityEngine;

public class PatrolState : FSMBaseState
{
    private Transform[] _paths;
    private GameObject _owner;
    private int _pathIndex;
    public PatrolState(FSMStates state,FSMSystem fsmSystem,Transform[] path,GameObject owner) : base(state,fsmSystem)
    {
        _paths = path;
        _pathIndex = 0;
        _owner = owner;
    }

    public override void BeforeEnteringState()
    {
        //for (int i = 0; i < _paths.Length; i++)
        //{
        //    if(Vector3.Angle(_owner.transform.position,new Vector3(_paths[i].position.x-_owner.transform.position.x,0,_paths[i].position.z - _owner.transform.position.z))<90f)
        //    {
        //        _pathIndex = i;
        //        break;
        //    }
        //}
    }
    public override void Action(GameObject owner, GameObject player)
    {
        if(Vector3.Distance(owner.transform.position,player.transform.position) <= 3)
        {
            fsmSystem.PerformTransition(FSMTransitions.LookPlayer);
        }
    }

    public override void Reason(GameObject owner, GameObject player)
    {
        Vector3 dir = new Vector3(_paths[_pathIndex].position.x, owner.transform.position.y, _paths[_pathIndex].position.z) - owner.transform.position;
        if(dir.magnitude<1)
        {
            _pathIndex++;
            if(_pathIndex>=_paths.Length)
            {
                _pathIndex = 0;
            }
        }
        owner.transform.Translate(dir.normalized * 5 * Time.deltaTime);


    }
}