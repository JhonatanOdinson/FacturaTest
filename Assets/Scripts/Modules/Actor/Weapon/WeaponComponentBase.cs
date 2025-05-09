using UnityEngine;

namespace Modules.Actor.Weapon
{
    public class WeaponComponentBase : MonoBehaviour
    {
        protected WeaponBase _weaponRef;

        public virtual void Init(WeaponBase weapon) {
            _weaponRef = weapon;
        }

        public virtual void Destruct() {
      
        }
    }
}
