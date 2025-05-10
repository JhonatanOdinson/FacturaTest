using Modules.Emitters;
using UnityEngine;

namespace Modules.PoolObjects.Instructions {
  [CreateAssetMenu(fileName = "Rotation", menuName = "ScriptableData/Emitter/EmitterInstruction/Rotation")]
  
  public class FollowRotationInstruction : PoolInstruction {
    
    public override void Invoke(PoolObjectParameter param) {
      param.PoolObj.IsFollowRotation = true;
    }
  }
}