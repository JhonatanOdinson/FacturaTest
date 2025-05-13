// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

#if UNITY_5_5_OR_NEWER

using HutongGames.PlayMaker;
using Modules.Actor.ActorComponent;
using UnityEngine;
using UnityEngine.AI;

namespace PlayMaker.Actions.Custom
{
	[ActionCategory(ActionCategory.NavMeshAgent)]
	[HutongGames.PlayMaker.Tooltip("Sets the stop or resume condition of the NavMesh agent. If set to True, the NavMesh agent's movement will be stopped along its current path. If set to False after the NavMesh agent has stopped, it will resume moving along its current path.")]
	public class CheckAgentOnNavmesh : FsmStateAction
	{
		[RequiredField]
		[HutongGames.PlayMaker.Tooltip("The Game Object to work with. NOTE: The Game Object must have a NavMeshAgent component attached.")]
		[CheckForComponent(typeof(ActorAI))]
		public FsmOwnerDefault gameObject;
		
		public FsmEvent onNavmesh;
		public FsmEvent NavmeshNotFound;

		private UnityEngine.AI.NavMeshAgent _agent;
		
		private void _getAgent()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) 
			{
				return;
			}
			
			_agent =  go.GetComponent<ActorAI>().NavMeshAgent;
		}	
		
		public override void Reset()
		{
			gameObject = null;

		}

		public override void OnEnter()
		{
			_getAgent();

			if (IsAgentOnNavMesh(_agent))
				Fsm.Event(onNavmesh);
			else 
				Fsm.Event(NavmeshNotFound);

			Finish();		
		}
		
		bool IsAgentOnNavMesh(NavMeshAgent agent)
		{
			NavMeshHit hit;
			return NavMesh.SamplePosition(agent.transform.position, out hit, 0.1f, NavMesh.AllAreas);
		}


	}
}

#endif