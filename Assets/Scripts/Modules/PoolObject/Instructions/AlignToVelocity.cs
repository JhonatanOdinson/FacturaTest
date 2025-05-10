using Modules.Emitters;
using UnityEngine;

namespace Modules.PoolObject.Instructions {
  [CreateAssetMenu(fileName = "AlignToVelocity", menuName = "ScriptableData/Emitter/EmitterInstruction/AlignToVelocity")]
  public class AlignToVelocity : PoolInstruction {
    public override void Invoke(PoolObjectParameter param) {
      base.Invoke(param);
      if (param.Tr.TryGetComponent(out Rigidbody rb)) {
        param.PoolObj.ActivateAlign(rb);
      }
    }
  }
}