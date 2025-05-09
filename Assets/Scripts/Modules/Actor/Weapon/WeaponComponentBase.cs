using UnityEngine;

namespace Modules.Actor.Weapon
{
    public class WeaponComponentBase : MonoBehaviour
    {
        protected WeaponBase _weaponRef;
        private bool _isEnabled = true;
        public bool IsEnable => _isEnabled;

        public virtual void Init(WeaponBase weapon) {
            _weaponRef = weapon;
        }

        public virtual void SetEnabled(bool state) {
            _isEnabled = state;
        }
        
        public virtual void UpdateExecute() {
        }
        
        public virtual void Destruct() {
      
        }
    }
}
