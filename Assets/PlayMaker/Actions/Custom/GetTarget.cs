using HutongGames.PlayMaker;
using Library.Scripts.Core;
using UnityEngine;

namespace PlayMaker.Actions.Custom {
  [ActionCategory("Custom")]
  public class GetTarget : FsmStateAction {
    public FsmOwnerDefault gameObject;
    public FsmGameObject target;
    public float range;
    public bool everyFrame = false;

    public override void Reset() {
      gameObject = null;
      target = null;
    }
    
    public override void OnEnter() {
      FindTarget();
      if(!everyFrame)
        Finish();
    }

    public override void OnUpdate() {
      FindTarget();
    }
    
    private void FindTarget() {
      var owner = Fsm.GetOwnerDefaultTarget(gameObject);
      var actorPlayer = CommonComponents.ActorBaseController.GetPlayer();
      
      if (!actorPlayer)
        target.Value = null;
      else {
        if( Distance(owner.transform, actorPlayer.transform) <= range)
          target.Value = actorPlayer.gameObject;
      }
    }
    
    private float Distance(Transform ownerT, Transform targetT) {
      return (ownerT.position - targetT.position).magnitude;
    }
  }
}