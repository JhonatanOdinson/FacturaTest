using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Modules.Emitters;
using UnityEngine;

namespace Modules.PoolObject {
  public class PoolDataBase : UniqueId {
    private GameObject _parent;
    
    public int StartCount = 1;
    
    [SerializeField] private List<PoolInstruction> _enableInstructions = new List<PoolInstruction>();
    [SerializeField] private List<PoolInstruction> _disableInstructions = new List<PoolInstruction>();

    public GameObject Parent {
      get { return _parent; }
      set { _parent = value; }
    }

    public async void ApplyEnableInstructions(PoolObjectParameter param) {
      await Execute(param, _enableInstructions);
    }

    public async Task ApplyDisableInstructions(PoolObjectParameter param, Action onFinish, bool instantly) {
      await Execute(param, _disableInstructions, instantly);
      onFinish.Invoke();
    }

    private async Task Execute(PoolObjectParameter param, List<PoolInstruction> instructions, bool instantly = false) {
      foreach (var action in instructions) {
        action.Invoke(param);
        await action.IsFinished(param, instantly);
      }
    }

    public virtual PoolBase InstantiatePoolObj(bool init, GameObject parent) { return null;}
    
    public virtual void ReleaseAsset(){}
    
  }
}