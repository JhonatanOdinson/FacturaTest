using Modules.Emitters;
using Modules.PoolObject;
using UnityEngine;

namespace Modules.PoolObjects.Instructions {
  [CreateAssetMenu(fileName = "BurstCoin", menuName = "ScriptableData/Emitter/EmitterInstruction/BurstCoin")]
  public class BurstInstruction : PoolInstruction{
    public override void Invoke(PoolObjectParameter param) {
      var emitter = (EmitterPoolSample)param.PoolObj;
      if(emitter == null) return;
      var emission = emitter.EmitterRef.emission;
      emission.rateOverTime = 0;
      emission.burstCount = 1;
      emission.SetBurst(0, new ParticleSystem.Burst(0, param.Count, 1, 0.03f));
    }
  }
}