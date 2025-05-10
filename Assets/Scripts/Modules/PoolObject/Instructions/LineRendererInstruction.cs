using Modules.Emitters;
using UnityEngine;

namespace Modules.PoolObject.Instructions {
  [CreateAssetMenu(fileName = "LineRenderer", menuName = "ScriptableData/Emitter/EmitterInstruction/LineRenderer")]
  public class LineRendererInstruction : PoolInstruction {
    public override void Invoke(PoolObjectParameter param) {
      var emitter = (EmitterLineRenderer)param.PoolObj;
      emitter.SetOwnerAndTargetTr(param.Tr, param.LineRendererTarget);
      emitter.SetLifeTime(param.LifeTime);
    }
  }
}