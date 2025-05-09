using Modules.Character;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Actor {
  
  public class ActorComponentBase : MonoBehaviour {
    [Title("Titles and Headers")]
    
    [SerializeField,HideInInspector] private ActorBase actorBase;
    private bool _isEnabled = true;
    public ActorBase ActorOwner => actorBase;
    public bool IsEnable => _isEnabled;

    public virtual void Init(ActorBase actorBase) {
      this.actorBase = actorBase;
    }

    public virtual void SetEnabled(bool state)
    {
      _isEnabled = state;
    }
    
    public virtual void UpdateExecute() {
    }

    public virtual void FixedUpdateExecute(float deltaTime) {
    }

    public virtual void Destruct() {
      
    }
  }
}