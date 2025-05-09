using Modules.Tool.UniversalPool;
using UnityEngine;

public class PoolableItem : MonoBehaviour, IPoolable {
  private IPool _pool;

  public void Initialize(IPool pool) {
    _pool = pool;
  }
  
  public virtual void New() {
  }

  public void Return() {
    _pool.Return(this);
  }
  
  public virtual void Free() {
    
  }
}
