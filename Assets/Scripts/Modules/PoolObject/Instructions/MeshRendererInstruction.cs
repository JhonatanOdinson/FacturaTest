using Modules.Emitters;
using UnityEngine;

namespace Modules.PoolObject.Instructions {
  [CreateAssetMenu(fileName = "SetMesh", menuName = "ScriptableData/Emitter/EmitterInstruction/SetMesh")]
  public class MeshRendererInstruction : PoolInstruction {
    public override void Invoke(PoolObjectParameter param) {
      var emitter = (EmitterPoolSample) param.PoolObj;
      if (emitter == null) return;
      var targetMesh = param.Tr.GetComponent<MeshRenderer>();
      if(targetMesh == null) return;
      var shape = emitter.EmitterRef.shape;
      shape.meshRenderer = targetMesh;
    }
  }
}