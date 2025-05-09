using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Modules.Tool {
  [Serializable]
  public class AssetReferenceInPrefab<TObject> : AssetReference where TObject : MonoBehaviour {
    public AssetReferenceInPrefab(string guid)
      : base(guid) {
/*#if UNITY_EDITOR
      m_DerivedClassType = typeof(TObject);
#endif*/
    }

    public override bool ValidateAsset(Object obj) {
      var go = obj as GameObject;
      return go != null && go.GetComponent<TObject>() != null;
    }

    public override bool ValidateAsset(string path) {
#if UNITY_EDITOR
      var go = AssetDatabase.LoadAssetAtPath<GameObject>(path);
      return go != null && go.GetComponent<TObject>() != null;
#else
      return false;
#endif
    }

    public async Task<TObject> LoadFromPrefab(bool instantly = false) {
      if (OperationHandle.IsDone && Asset != null) {
        return ((GameObject)Asset).GetComponent<TObject>();
      }
        
      var load = base.LoadAssetAsync<GameObject>();
      if (!instantly)
        await load.Task;
      else
        load.WaitForCompletion();

      return load.Result.GetComponent<TObject>();
    }

    public override void ReleaseAsset() {
      if (!OperationHandle.IsDone || Asset == null) return;
      base.ReleaseAsset();
    }

    /// <summary>
    /// Not use this method for this addressable type. Use LoadFromPrefab instead
    /// </summary>
    [Obsolete]
    public override AsyncOperationHandle<T> LoadAssetAsync<T>() {
      throw new Exception("Method is not used for this type. Use LoadFromPrefab instead");
    }
  }
}