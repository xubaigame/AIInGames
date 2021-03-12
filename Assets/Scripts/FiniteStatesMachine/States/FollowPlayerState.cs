/****************************************************
    文件：FollowPlayerState.cs
	作者：积极向上小木木
    邮箱：positivemumu@126.com
    日期：2020/11/10 22:23:32
	功能：Nothing
*****************************************************/

using UnityEngine;

namespace FiniteStatesMachine
{
	public class FollowPlayerState : FSMBaseState
	{
		private Transform _owner;
		private Transform _player;
		public FollowPlayerState(string name, FSMSystem fsmSystem,Transform owner,Transform player) : base(name, fsmSystem) 
		{
			_owner = owner;
			_player = player;
		}

		public override void Action()
		{
			if (Vector3.Distance(_owner.transform.position, _player.transform.position) >= 15)
			{
				fsmSystem.PerformTransition(Constants.MissPlayer);
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
			
		}
	}
}
