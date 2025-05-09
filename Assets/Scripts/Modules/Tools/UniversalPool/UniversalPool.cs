using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Modules.Tool.UniversalPool {
  [Serializable]
  public class UniversalPool<T> : IPool {
    [SerializeField] private PoolableItem _prefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private int _size = 4;
    [SerializeField] private int _countCreate = 0;

    private Queue<T> _poolFreeQueue;
    [SerializeField] private List<T> _poolBusyList = new List<T>();
    [SerializeField] [ReadOnly] private List<T> _listElemets = new List<T>();

    public delegate int Comparsion(T x, T y);

    public UniversalPool(int countCreateCreate, int poolSize) {
      Initialize(countCreateCreate, poolSize);
    }

    public UniversalPool(PoolableItem prefab, Transform parent, int countCreateCreate, int poolSize) {
      _prefab = prefab;
      _parent = parent;
      Initialize(countCreateCreate, poolSize);
    }

    public void Initialize() {
      _poolFreeQueue = new Queue<T>(_size);
      CreateElements(_countCreate);
    }

    private void Initialize(int countCreateCreate, int poolSize) {
      _countCreate = countCreateCreate;
      _size = poolSize;
      Initialize();
    }

    private void CreateElements(int count) {
      for (int i = 0; i < count; i++) {
        _poolFreeQueue.Enqueue(Create());
      }
    }

    public IEnumerable<T> GetBusy() {
      return _poolBusyList;
    }

    public void SortBusy(Comparsion c) {
      _poolBusyList.Sort((x, y) => c(x, y));
    }   
    
    private T Create() {
      var obj = (T) (object) Object.Instantiate((Object) _prefab, _parent);
      IPoolable item = (IPoolable) obj;
      item.Initialize(this);
      item.New();
      _listElemets.Add(obj);
      return obj;
    }

    public T Take() {
      T result;
      if (_poolFreeQueue.Count > 0) {
        result = (T) _poolFreeQueue.Dequeue();
      }
      else {
        result = Create();
      }

      _poolBusyList.Add(result);
      return result;
    }

    public void Return(IPoolable obj) {
      obj.Free();
      _poolFreeQueue.Enqueue((T) obj);
      _poolBusyList.Remove((T) obj);
    }

    public void Clear() {
      _poolBusyList.Clear();
      _poolFreeQueue.Clear();
      _listElemets.ForEach(e => {
        ((IPoolable) e).Free();
        _poolFreeQueue.Enqueue(e);
      });
    }
  }
}