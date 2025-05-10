using Modules.Emitters;
using UnityEngine;

namespace Modules.PoolObject.Instructions {
  [CreateAssetMenu(fileName = "RandomFloatInstruction", menuName = "ScriptableData/Emitter/EmitterInstruction/RandomFloatInstruction")]
  public class RandomFloatInstruction : PoolInstruction {
    [SerializeField] private string _fieldName;
    [SerializeField] private float _min;
    [SerializeField] private float _max;

    public override void Invoke(PoolObjectParameter param) { 
      var renderer = param.PoolObj.gameObject.GetComponent<Renderer>();
      if(renderer == null) return;
      float rnd = Random.Range(_min, _max);
      renderer.material.SetFloat(_fieldName, rnd);
    }
  }
}