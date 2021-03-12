/****************************************************
    文件：NPCController.cs
	作者：积极向上小木木
    邮箱：positivemumu@126.com
    日期：2020/11/10 23:8:5
	功能：Npc控制类
*****************************************************/

using UnityEngine;

public class NPCController : MonoBehaviour 
{
    public GameObject player;
    public Transform[] path;
    private FSMSystem fsm;

    public void Start()
    {
        MakeFSM();
    }

    public void MakeFSM()
    {
        fsm = new FSMSystem();
        FollowPlayerState followPlayerState = new FollowPlayerState(FSMStates.FollowPlayerState,fsm,transform,player.transform);
        followPlayerState.AddTransition(FSMTransitions.MissPlayer, FSMStates.PartolState);

        PatrolState patrolState = new PatrolState(FSMStates.PartolState,fsm, path,transform,player.transform);
        patrolState.AddTransition(FSMTransitions.LookPlayer, FSMStates.FollowPlayerState);

        fsm.AddState(patrolState);
        fsm.AddState(followPlayerState);
    }

    public void FixedUpdate()
    {
        fsm.CurrentState.Reason();
        fsm.CurrentState.Action();
    }
}