using Modules.Actor.Components;
using Modules.Character;
using Modules.Damage;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Modules.Actor.Weapon
{
  public class WeaponMelee : WeaponBase
  {
    [SerializeField] [FoldoutGroup("Actions")]
    protected UnityEvent _onAttack;

    public override void Attack(ActorBase target)
    {
      base.Attack(target);
      ReceiveDamage(target.DamageReceiver);
    }

    private void ReceiveDamage(DamageReceiver damageReceiver)
    {
      damageReceiver.ReceiveDamage(new DamageData(_weaponDataEx, _weaponDataEx.Data.AttackStat));
      _actorRef.DamageReceiver.ReceiveDamage(new DamageData(null,_actorRef.Data.MaxVitality));
      _onAttack?.Invoke();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
      var receiveDamage = other.gameObject.GetComponent<IReceiveDamage>();

      if (receiveDamage == null) return;
      if (receiveDamage is DamageReceiver { Owner: ActorBase actorBase })
      {
        if (actorBase == ActorRef) return;
        if (!actorBase.Data.IsPlayer) return;
        ReceiveDamage(actorBase.DamageReceiver);
      }
    }

    protected virtual bool CheckCanDamageCollision(Collision collision)
    {
      if (collision.impulse.magnitude < 0.05f) return false;
      return true;
    }
  }
}
