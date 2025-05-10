using System.Threading;
using Library.Scripts.Core;
using Modules.Actor.Components;
using Modules.Character;
using Modules.Damage;
using UnityEngine;

namespace Modules.Actor {
  public class Damager : MonoBehaviour {
    public enum CheckTypeE {
      OnCollision = 0,
      OnTrigger = 1,
      Both = 2
    }
    [SerializeField] protected int _damageCount;
    [SerializeField, Tooltip("If true not take damage from actor data, if it is owner")] 
    private bool _useCustomDamageCount;
    [SerializeField] protected CheckTypeE _checkType;

    private CancellationTokenSource _source = new();
    private object _owner;

    public void Init(object owner) {
      _owner = owner;
      _damageCount = GetActorOwner() != null && !_useCustomDamageCount ? GetActorOwner().Data.GetCurrAttack : _damageCount;
    }

    protected virtual void OnCollisionEnter(Collision collision) {
      if(_checkType == CheckTypeE.OnTrigger) return;
      if (collision.impulse.magnitude < 0.05f) return;
      CheckForDamage(collision.gameObject, collision.GetContact(0).point,collision.collider);
    }

    protected virtual void OnTriggerEnter(Collider other) {
      if(_checkType == CheckTypeE.OnCollision) return;
      if (GameDirector.GetEnterPoint.DestructProcess) return;
      GameObject targetGO = other.gameObject;
      if (other.gameObject.GetComponent<IReceiveDamage>() == null && other.transform.parent != null)
        targetGO = other.transform.parent.gameObject;
      CheckForDamage(targetGO, other.ClosestPoint(transform.position),other);
      //OnCharacterFall?.Check(_customOwner,null);
    }

    protected async void CheckForDamage(GameObject go, Vector3 contactPoint,Collider collider) {
      var actorOwner = GetActorOwner();
      var receiveDamage = go.GetComponent<IReceiveDamage>();
      if (receiveDamage == null) return;
      if(receiveDamage is DamageReceiver {Owner: ActorBase actor} && actor == actorOwner) return;
      if(_damageCount > 0)
        receiveDamage.ReceiveDamage(new DamageData(_owner, _damageCount));
    }

    public ActorBase GetActorOwner() {
      if (_owner is ActorBase actor) return actor;
      //if (_owner is WeaponDataEx weaponDataEx) return weaponDataEx.GetOwner;
      return null;
    }

    public void FreeOwner() {
      _owner = null;
    }

    protected virtual void OnDestroy() {
      _source.Cancel();
      _source.Dispose();
    }
  }
}
