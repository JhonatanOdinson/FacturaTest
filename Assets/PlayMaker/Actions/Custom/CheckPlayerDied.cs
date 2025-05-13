// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.

using HutongGames.PlayMaker;
using Library.Scripts.Core;
using Modules.Actor.ActorComponent;
using UnityEngine;

namespace PlayMaker.Actions.Custom
{
	[ActionCategory(ActionCategory.NavMeshAgent)]
	[HutongGames.PlayMaker.Tooltip("Set the destination of a NavMesh Agent to a gameObject. \nNOTE: The Game Object must have a NavMeshAgentcomponent attached.")]
	public class CheckPlayerDied : FsmStateAction
	{
		public FsmEvent isAlive;
		public FsmEvent isDied;
		
		public bool everyFrame;

		private bool CheckPlayerStatus()
		{
			return CommonComponents.ActorBaseController.GetPlayer().Data.IsDead;
		}	
		
		public override void Reset()
		{
			
			
		}

		public void CheckEvent()
		{
			if(CheckPlayerStatus())
				Fsm.Event(isDied);
			else 
				Fsm.Event(isAlive);
		}

		public override void OnEnter()
		{
			CheckEvent();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			CheckEvent();
		}


	}
}