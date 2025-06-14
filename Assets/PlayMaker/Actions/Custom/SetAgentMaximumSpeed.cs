// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

using Modules.Actor.ActorComponent;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.NavMeshAgent)]
	[Tooltip("Set the maximum movement speed of a NavMesh Agent. \n" +
		"NOTE: The Game Object must have a NavMeshAgent component attached.")]
	public class SetAgentMaximumSpeed : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Game Object to work with. NOTE: The Game Object must have a NavMeshAgent component attached.")]
		[CheckForComponent(typeof(ActorAI))]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("The maximum movement speed of the navMesh Agent.")]
		public FsmFloat maximumSpeed;

		private UnityEngine.AI.NavMeshAgent _agent;
		
		private void _getAgent()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) 
			{
				return;
			}

			_agent = go.GetComponent<ActorAI>().NavMeshAgent;
		}	
		
		public override void Reset()
		{
			gameObject = null;
			maximumSpeed = null;

		}

		public override void OnEnter()
		{
			_getAgent();
			
			DoSetMaximumSpeed();

			Finish();		
		}
		
		void DoSetMaximumSpeed()
		{
			if (maximumSpeed == null || _agent == null) 
			{
				return;
			}
			
			_agent.speed = maximumSpeed.Value;
		}

	}
}