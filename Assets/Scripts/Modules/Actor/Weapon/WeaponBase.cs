using System;
using System.Collections.Generic;
using Modules.Character;
using UnityEngine;

namespace Modules.Actor.Weapon
{
    public class WeaponBase : MonoBehaviour
    {
        protected WeaponDataEx _weaponDataEx;
        protected ActorBase _actorRef;
        private WeaponManager _weaponManager;
        private bool _autoAttack;

        private float _currentCooldown;
        protected bool _waitingCooldown;
        
        [SerializeField] private List<WeaponComponentBase> _weaponComponents = new();
        
        public Action OnAttack;
        public Action<float, float> OnCooldownUpdate;
        public Action<float, float> OnChargeProcess;
        
        public ActorBase ActorRef => _actorRef;
        public WeaponDataEx WeaponDataEx => _weaponDataEx;
        
        public virtual WeaponSettingsData WeaponSettings => WeaponDataEx.Data.WeaponSettings;

        public virtual void Init(ActorBase actorRef, WeaponDataEx weaponDataEx)
        {
            _actorRef = actorRef;
            _weaponDataEx = weaponDataEx;
            _autoAttack = weaponDataEx.Data.AutoFire;
            
            weaponDataEx.Init(this);
            
            _weaponManager = actorRef.Components.FetchComponent<WeaponManager>();
            _weaponComponents.ForEach(e => e.Init(this));
            ApplyPositionAndRotation();
            
        }

        private void ApplyPositionAndRotation()
        {
            transform.localPosition = WeaponSettings.StartPositionOffset;
            transform.localEulerAngles = WeaponSettings.StartRotationOffset;
        }
        
        public virtual void Attack(/*Transform target*/) {
            WaitCooldown();
            OnAttack?.Invoke();
        }
        
        public virtual bool CanAttack() {
            return !_waitingCooldown;
        }
        
        public virtual void UpdateExecute()
        {
            UpdateComponents();
            if (_waitingCooldown) {
                _currentCooldown -= Time.deltaTime;
                OnCooldownUpdate?.Invoke(_currentCooldown, _weaponManager.GetAttackCooldown());
                if (_currentCooldown <= 0) {
                    _currentCooldown = 0;
                    _waitingCooldown = false;
                }
            }
            else if(_autoAttack)
            {
                Attack();
            }
        }

        private void UpdateComponents()
        {
            for (int i = _weaponComponents.Count - 1; i >= 0; i--) {
                _weaponComponents[i].UpdateExecute();
            }
        }

        protected void WaitCooldown() {
            if(_waitingCooldown) return;
            _waitingCooldown = true;
            _currentCooldown = _weaponManager.GetAttackCooldown();
        }
        
        public virtual void Destruct() {
            _weaponComponents.ForEach(e => e.Destruct());
        }
    
        public T FetchComponent<T>() where T : WeaponComponentBase {
            WeaponComponentBase componentBase = _weaponComponents.Find(e => e.GetType() == typeof(T));
            return (T)componentBase;
        }
        
        
    }
}
