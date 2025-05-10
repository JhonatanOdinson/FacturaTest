using UnityEngine;

namespace Modules.Emitters.EmitterInstruction {
  [CreateAssetMenu(fileName = "Follow", menuName = "ScriptableData/Emitter/EmitterInstruction/Follow")]
  public class FollowInstruction : PoolInstruction {
    public override void Invoke(PoolObjectParameter param) {
      if (param.Tr != null) {
        param.PoolObj.FollowTransform = param.Tr;
      }
    }
  }
}