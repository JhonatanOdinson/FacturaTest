using System.Threading.Tasks;
using DG.Tweening;
using Modules.Emitters;
using UnityEngine;

namespace Modules.PoolObjects.Instructions {
  [CreateAssetMenu(fileName = "DoScale", menuName = "ScriptableData/Emitter/EmitterInstruction/DoScale")]
  public class DoScaleInstruction : PoolInstruction {
    [SerializeField] private float _time;
    [SerializeField] private Vector3 _scale;
    [SerializeField] private bool _resetToZero;
    
    public override async Task IsFinished(PoolObjectParameter param, bool instantly) {
      var tr = param.PoolObj.transform;
      if (instantly) {
        tr.localScale = _scale;
        return;
      }
      var completionSource = new TaskCompletionSource<bool>();
      if(_resetToZero) tr.localScale = Vector3.zero;
      tr.DOScale(_scale, _time).OnComplete(() => {
        completionSource.SetResult(true);
      });
      await completionSource.Task;
    }
  }
}