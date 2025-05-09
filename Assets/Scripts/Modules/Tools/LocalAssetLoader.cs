using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Library.Scripts.Modules.Tools {
  public static class LocalAssetLoader {
    /// <summary>
    /// Instantiate an asset from Addressables. In constructors need to be used instantly
    /// </summary>
    /// <param name="assetId">asset address</param>
    /// <param name="instantly">if true-return asset with WaitForCompilation</param>
    /// <typeparam name="T">return type</typeparam>
    /// <returns></returns>
    public static async Task<T> InstantiateAsset<T>(string assetId, Transform parent = null, bool instantly = false) {
      var handle = Addressables.InstantiateAsync(assetId, parent);
      GameObject cachedObject = !instantly ? await handle.Task : handle.WaitForCompletion();
      if (cachedObject.TryGetComponent(out T component) == false)
        throw new NullReferenceException($"Object of type {typeof(T)} is null on " +
                                         "attempt to load it from addressables");
      return component;
    }
    
    public static async Task<T> InstantiateAsset<T>(AssetReference asset, Transform parent = null, bool instantly = false) {
      var handle = Addressables.InstantiateAsync(asset, parent);
      GameObject cachedObject = !instantly ? await handle.Task : handle.WaitForCompletion();
      if (cachedObject.TryGetComponent(out T component) == false)
        throw new NullReferenceException($"Object of type {typeof(T)} is null on " +
                                         "attempt to load it from addressables");
      return component;
    }

    /// <summary>
    /// Give an asset from Addressables. In constructors need to be used instantly
    /// </summary>
    /// <param name="assetId">asset address </param>
    /// <param name="instantly">if true-return asset with WaitForCompilation</param>
    /// <typeparam name="T">return type</typeparam>
    /// <returns></returns>
    public static async Task<T> LoadAsset<T>(string assetId, bool instantly = false) where T : Object {
      if (IsMonoBehavior(typeof(T))) {
        var goAsset = GetAssetFromGameObject<T>(assetId, instantly);
        await goAsset;
        return goAsset.Result;
      }

      var load = Addressables.LoadAssetAsync<T>(assetId);
      if (!instantly)
        await load.Task;
      else
        load.WaitForCompletion();
      return load.Result;
    }

    /// <summary>
    /// Give List of T from Addressables. In constructors need to be used instantly
    /// </summary>
    /// <param name="key">assets key</param>
    /// <param name="instantly">if true-return assets with WaitForCompilation</param>
    /// <typeparam name="T">return list type</typeparam>
    /// <returns></returns>
    public static async Task<List<T>> LoadAssets<T>(string key, bool instantly = false) {
      if (IsMonoBehavior(typeof(T))) {
        var goAsset = GetAssetsFromGameObject<T>(key, instantly);
        await goAsset;
        return goAsset.Result;
      }

      var resultList = new List<T>();
      var load = Addressables.LoadAssetsAsync<T>(key, asset => resultList.Add(asset));
      if (!instantly)
        await load.Task;
      else
        load.WaitForCompletion();
      return resultList;
    }

    private static async Task<T> GetAssetFromGameObject<T>(string assetId, bool instantly) {
      var load = Addressables.LoadAssetAsync<GameObject>(assetId);
      if (!instantly)
        await load.Task;
      else
        load.WaitForCompletion();
      var result = load.Result;
      if (result.TryGetComponent(out T component) == false)
        throw new NullReferenceException($"Object of type {typeof(T)} is null on " +
                                         "attempt to load it from addressables");
      return component;
    }

    private static async Task<List<T>> GetAssetsFromGameObject<T>(string key, bool instantly) {
      var load = Addressables.LoadAssetsAsync<GameObject>(key, null);
      if (!instantly)
        await load.Task;
      else
        load.WaitForCompletion();

      if (typeof(T) == typeof(GameObject))
        return load.Result as List<T>;

      var resultList = new List<T>();
      foreach (var loadE in load.Result) {
        if (loadE.TryGetComponent(out T component))
          resultList.Add(component);
      }

      return resultList;
    }

    private static bool IsMonoBehavior(Type type) {
      return type == typeof(MonoBehaviour) || type.IsSubclassOf(typeof(MonoBehaviour));
    }

    public static void UnloadInternal(GameObject gameObject) {
      if (gameObject == null)
        return;
      gameObject.SetActive(false);
      Addressables.ReleaseInstance(gameObject);
    }
  }
}