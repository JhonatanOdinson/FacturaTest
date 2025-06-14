using Modules.Character;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Actor.Weapon
{
    public class WeaponManager : ActorComponentBase
    {
        [ShowInInspector] private WeaponBase _weapon;
        [SerializeField] private Transform _weaponContainer;

        public WeaponBase Weapon => _weapon;
        
        public override void Init(ActorBase actorBase) {
            base.Init(actorBase);
            SetWeaponFromData(actorBase);
        }

        public override void SetEnabled(bool state)
        {
            base.SetEnabled(state);
            _weapon.SetEnabled(state);
        }

        private void SetWeaponFromData(ActorBase actorBase) {
            if (actorBase.Data.WeaponDataEx == null) return;
            var weaponData = actorBase.Data.WeaponDataEx;
            if (weaponData == null) return;
            SetWeapon(weaponData);
        }
        
        public override void UpdateExecute() {
            if(!IsEnable) return;
            if (_weapon != null) _weapon.UpdateExecute();
        }
        
        private void RemoveWeapon() {
            if (_weapon == null) return;
            _weapon.Destruct();
            Destroy(_weapon.gameObject);
        }

        public void Attack(ActorBase target) {
            if (_weapon == null) return;
            _weapon.Attack(target);
        }
        
        public void SetWeapon(WeaponDataEx weaponDataEx)
        {
            if (_weapon != null)
            {
                RemoveWeapon();
            }

            var weapon = weaponDataEx.Data.InstantiatePrefab(_weaponContainer);
            weapon.Init(ActorOwner, weaponDataEx);
            if (ActorOwner != null) ActorOwner.Data.SetWeapon(weapon.WeaponDataEx);
            _weapon = weapon;
        }

        public float GetAttackCooldown()
        {
           return _weapon.WeaponDataEx.Data.AttackCooldown;
        }
    }
}
