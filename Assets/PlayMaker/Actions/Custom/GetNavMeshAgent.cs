using HutongGames.PlayMaker;
using UnityEngine;
using UnityEngine.AI;

namespace PlayMaker.Actions.Custom {
  [ActionCategory("Custom")]
  public class GetNavMeshAgent : FsmStateAction {
    
    public FsmOwnerDefault gameObject;
    public FsmObject result;

    public override void Reset() {
      gameObject = null;
      result = null;
    }
    
    public override void OnEnter() {
      //if (gameObject.GameObject.Value != null) {
        result.Value = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<NavMeshAgent>();
        //Debug.Log($"Get Nav mesh Agent {result.Value}");
      //}
      Finish();
    }
    
  }
}
