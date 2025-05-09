using System;
using System.Collections.Generic;
using System.Linq;
using Modules.Character;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Actor {
  public class ActorComponents : MonoBehaviour {
    [SerializeField] private List<ActorComponentBase> _components = new List<ActorComponentBase>();
    private bool _isInit = false;
    private ActorBase _actorBasedRef;

    public void Init(ActorBase actorBase) {
      _actorBasedRef = actorBase;
      for (int i = _components.Count - 1; i >= 0; i--) {
        if (_components[i] != null)
          _components[i].Init(actorBase);
      }
      _isInit = true;
    }

    public void UpdateEnableComponents(bool state)
    {
      _components.ForEach(e => e.SetEnabled(state));
    }

    public void UpdateExecute() {
      if(!_isInit) return;
      for (int i = _components.Count - 1; i >= 0; i--) {
        //if (_components[i] != null)
          _components[i].UpdateExecute();
      }
    }
    
    public void FixedUpdateExecute(float deltaTime) {
      if(!_isInit) return;
      //if(actorBaseRef.Object!=null && !actorBaseRef.Object.Runner.IsForward) return;
      for (int i = _components.Count - 1; i >= 0; i--) {
        //if (_components[i]!= null)
          _components[i].FixedUpdateExecute(deltaTime);
      }
    }

    public T FetchComponent<T>() where T : ActorComponentBase {
      ActorComponentBase componentBase= _components.Find(e => e.GetType() == typeof(T));
      return (T)Convert.ChangeType(componentBase, typeof(T));
    }
    
    public List<T> FetchComponents<T>() where T : ActorComponentBase {
      List<ActorComponentBase> componentBases = _components.FindAll(e => e.GetType() == typeof(T));
      List<T> resultList = new List<T>();

      foreach (var component in componentBases) {
        resultList.Add((T)component);
      }

      return resultList;
    }

    
    [Button]
    private void FindAllActorComponents() {
      _components = transform.parent != null
        ? transform.parent.GetComponentsInChildren<ActorComponentBase>().ToList()
        : GetComponentsInChildren<ActorComponentBase>().ToList();
    }
    public void Destruct() {
      _components.ForEach(e => e.Destruct());
    }
  }
}