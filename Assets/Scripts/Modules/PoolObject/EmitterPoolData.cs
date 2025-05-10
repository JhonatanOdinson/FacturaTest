using Modules.PoolObject;
using Modules.Tool;
using UnityEngine;

namespace Modules.Emitters {
  [CreateAssetMenu(fileName = "NewEmitterData", menuName = "ScriptableData/Emitter/NewEmitterData")]
  public class EmitterPoolData : PoolDataBase {
    [SerializeField] private AssetReferenceInPrefab<EmitterPoolSample> _emitterRef;

    public override PoolBase InstantiatePoolObj(bool init, GameObject parent) {
      var newGO = Instantiate(_emitterRef.LoadFromPrefab(true).Result, parent.transform);
      newGO.name = _emitterRef.Asset.name;
      newGO.Init();
      newGO.gameObject.SetActive(false);
      if (init) ObjectPoolController.PoolDict[this].Enqueue(newGO);
      return newGO;
    }

    public override void ReleaseAsset() {
      base.ReleaseAsset();
      _emitterRef.ReleaseAsset();
    }
  }
}