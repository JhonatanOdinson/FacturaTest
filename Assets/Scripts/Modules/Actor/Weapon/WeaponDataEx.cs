using System;
using Modules.Character;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Actor.Weapon
{
    [Serializable]
    public class WeaponDataEx
    {
        [ShowInInspector, ReadOnly] private WeaponData _data;
        [ShowInInspector, ReadOnly] private WeaponBase _weaponRef;
        
        public WeaponData Data => _data;
        public WeaponBase WeaponRef => _weaponRef;
        
        public ActorBase GetOwner => _weaponRef.ActorRef;
        
        public WeaponDataEx(WeaponData weaponData) {
            _data = weaponData;
        }
        
        public void Init(WeaponBase weaponRef) {
            _weaponRef = weaponRef;
        }
    }
}
