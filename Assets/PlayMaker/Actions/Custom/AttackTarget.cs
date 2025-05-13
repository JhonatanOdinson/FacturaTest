// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

using HutongGames.PlayMaker;
using Library.Scripts.Core;
using Modules.Actor.ActorComponent;
using Modules.Character;
using UnityEngine;

namespace PlayMaker.Actions.Custom
{
	[ActionCategory(ActionCategory.NavMeshAgent)]
	public class AttackTarget : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(ActorAI))]
		public FsmOwnerDefault gameObject;

		public bool attackPlayer;
		[HideIf("attackPlayer")] public FsmGameObject target;

		private ActorBase _actor;

		private void _getActorOwner()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

			_actor = go.GetComponent<ActorAI>().ActorOwner;
		}

		public override void Reset()
		{
			gameObject = null;
		}

		public override void OnEnter()
		{
			_getActorOwner();

			if(attackPlayer)
				_actor.Attack(CommonComponents.ActorBaseController.GetPlayer());
		}
	}
}