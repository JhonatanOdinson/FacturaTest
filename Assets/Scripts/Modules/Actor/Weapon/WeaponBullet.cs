using System;
using System.Collections.Generic;
using Modules.Actor.Components;
using Modules.Character;
using Modules.Damage;
using UnityEngine;

namespace Modules.Actor.Weapon
{
    public class WeaponBullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigid;
        [SerializeField] private float _lifeTime = 1f;
        [SerializeField] private bool _destroyOnCollision;
        
        /*[SerializeField] private PoolDataBase _SpawnParticles;
        [SerializeField] private PoolDataBase _CollisionParticles;
        [SerializeField] private PoolDataBase _DestroyParticles;*/
        
        protected WeaponDataEx _weaponDataEx;
        private float _currTime;
        private IReceiveDamage _damaged;

        public Action OnDestroy;
        
        public virtual void Init(WeaponDataEx weaponDataEx) {
            _weaponDataEx = weaponDataEx;
            //ObjectPoolController.SpawnObject(new PoolObjectParameter(_SpawnParticles, transform.position, transform.rotation));
        }
        
        protected void Update() {
            if (_currTime >= _lifeTime) {
                Destruct();
                return;
            }

            _currTime += Time.deltaTime;
        }
        
        protected virtual void OnCollisionEnter(Collision collision) {
            var receiveDamage = collision.gameObject.GetComponent<IReceiveDamage>();
            if (receiveDamage == null || _damaged == receiveDamage) return;
            //if(_weaponDataEx.GetOwner.HasDamageReceiver(receiveDamage) || receiveDamage == _weaponDataEx.WeaponRef) return;
            receiveDamage.ReceiveDamage(new DamageData(_weaponDataEx, _weaponDataEx.GetOwner.Data.GetCurrAttack));
            if (receiveDamage is DamageReceiver {Owner: ActorBase actor} && !actor.Data.IsDead) {
               _damaged = actor.DamageReceiver; //for not to apply more than 1 damage to actor
            }
            //ObjectPoolController.SpawnObject(new PoolObjectParameter(_CollisionParticles, transform.position, transform.rotation));
            if (_destroyOnCollision)
                Destruct();
            //_damagedList.Add(receiveDamage);
        }

        public void AddForce(float force, Transform firePoint)
        {
            _rigid.AddForce(firePoint.forward * force * _rigid.mass);
        }
        
        protected void Destruct() {
            OnDestroy?.Invoke();
            //ObjectPoolController.SpawnObject(new PoolObjectParameter(_DestroyParticles, transform.position, transform.rotation));
            //Destroy(gameObject);
        }
    }
}
