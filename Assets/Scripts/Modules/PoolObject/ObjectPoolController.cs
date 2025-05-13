using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Modules.Emitters;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Modules.PoolObject {
  public class ObjectPoolController : MonoBehaviour {
    [ShowInInspector]public static Dictionary<PoolDataBase, Queue<PoolBase>> PoolDict = new Dictionary<PoolDataBase, Queue<PoolBase>>();
    [ShowInInspector] private static List<PoolBase> _activeObjects = new();

    public async Task LoadData() {
      await LoadAllObject();
    }

    private async Task LoadAllObject() {
      await Addressables.LoadAssetsAsync<PoolDataBase>("PoolObject", poolData => {
        PoolDict.Add(poolData, new Queue<PoolBase>());
        poolData.Parent = CreateParent(poolData.name);
        for (int i = 1; i <= poolData.StartCount; i++) {
          CreateNewObject(poolData, poolData.Parent, true);
        }
      }).Task;
    }

    private GameObject CreateParent(string name) {
      var newGO = new GameObject(name);
      newGO.transform.SetParent(transform);
      return newGO;
    }
    
    public static PoolBase SpawnObject(PoolObjectParameter param) {
      if (param.PoolData == null) return null;
      var pObj = GetPoolObject(param.PoolData);
      pObj.SetParameter(param);
      pObj.ActivateObj();
      param.PoolObj = pObj;
      param.PoolData.ApplyEnableInstructions(param);
      _activeObjects.Add(pObj);
      return pObj;
    }

    public async void StopObject(PoolDataBase poolDataBase, Transform parent) {
      for (int i = _activeObjects.Count - 1; i >= 0; i--) {
        if (_activeObjects[i].Data == poolDataBase && _activeObjects[i].CheckTransform(parent)) {
          if(_activeObjects[i] is EmitterPoolSample || _activeObjects[i].GetType().IsSubclassOf(typeof(EmitterPoolSample)))
            ((EmitterPoolSample)_activeObjects[i]).FreeEmitter();
          else
            await ReturnObject(_activeObjects[i]);
        }
      }
    }

    private static PoolBase GetPoolObject(PoolDataBase obj) {
      if (PoolDict.TryGetValue(obj, out Queue<PoolBase> objectList)) {
        if (objectList.Count == 0) {
          var newObj = CreateNewObject(obj, obj.Parent);
          return newObj; 
        }
        else {
          PoolBase _obj = objectList.Dequeue();
          return _obj;
        }
      }
      else {
        var newObj = CreateNewObject(obj, obj.Parent);
        return newObj;
      }
    }

    private static PoolBase CreateNewObject(PoolDataBase obj, GameObject parent, bool init = false) {
      return obj.InstantiatePoolObj(init, parent);
    }

    public async Task ReturnObject(PoolBase obj, bool instantly = false) {
      if(obj == null) return;
      foreach (var el in PoolDict) {
        if (el.Key == obj.Data && !el.Value.Contains(obj)) {
          await el.Key.ApplyDisableInstructions(new PoolObjectParameter(poolObj:obj), () => {
            obj.OnFinish();
            el.Value.Enqueue(obj);
            obj.DeactivateObj();
            DOTween.Kill(obj.transform, true);
            _activeObjects.Remove(obj);
          }, instantly);
        }
      }
    }

    public async void FreeAllObject() {
      for (int i = _activeObjects.Count - 1; i >= 0; i--) {
        if(_activeObjects[i] is EmitterPoolSample || _activeObjects[i].GetType().IsSubclassOf(typeof(EmitterPoolSample)))
          ((EmitterPoolSample)_activeObjects[i]).FreeEmitter(true);
        else
          await ReturnObject(_activeObjects[i], true);
      }
      
      foreach (var el in PoolDict) {
        while (el.Value.Count > el.Key.StartCount) {
          var temp = el.Value.Dequeue();
          if(temp != null) Destroy(temp.gameObject);
        }
        if(el.Key.StartCount == 0)
          el.Key.ReleaseAsset();
      }
    }
  }
}