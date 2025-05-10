using System.Threading.Tasks;
using DG.Tweening;
using Modules.Emitters;
using UnityEngine;

namespace Modules.PoolObject.Instructions {
  [CreateAssetMenu(fileName = "MaterialFieldTween", menuName = "ScriptableData/Emitter/EmitterInstruction/MaterialFieldTween")]
  public class MaterialFloatTween : PoolInstruction {
    [SerializeField] private string _fieldName;
    [SerializeField] private float _from;
    [SerializeField] private float _to;
    [SerializeField] private float _tweenTime;

    public override async Task IsFinished(PoolObjectParameter param, bool instantly) {
      var renderer = param.PoolObj.gameObject.GetComponent<Renderer>();
      if(renderer == null) return;
      if (instantly) {
        renderer.material.SetFloat(_fieldName, _to);
        return;
      }
      var completionSource = new TaskCompletionSource<bool>();
      renderer.material.SetFloat(_fieldName, _from);
      renderer.material.DOFloat(_to, _fieldName, _tweenTime).OnComplete(() => {
        completionSource.SetResult(true);
      });
      await completionSource.Task;
    }
  }
}