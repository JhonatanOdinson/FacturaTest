using System;
using Modules.Actor.Components;
using Modules.ActorObject;
using Modules.ActorObject.ActorObjectSpawnData;
using Modules.Character;
using Modules.Damage;
using Modules.Emitters;
using Modules.PoolObject;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Modules.Actor.Weapon
{
    public class WeaponBullet : ActorObjectBase
    {
        [SerializeField] private Rigidbody _rigid;
        [SerializeField] private float _lifeTime = 1f;
        [SerializeField] private bool _destroyOnCollision;

        [SerializeField] private PoolDataBase _SpawnParticles;
        [SerializeField] private PoolDataBase _CollisionParticles;
        [SerializeField] private PoolDataBase _DestroyParticles;

        protected WeaponDataEx _weaponDataEx;
        [SerializeField] private float _currTime;
        private IReceiveDamage _damaged;
        [SerializeField] private bool _isFree;

        public override void Init(object spawnData)
        {
            BulletSpawnData bulletSpawnData = (BulletSpawnData)spawnData;
            _weaponDataEx = bulletSpawnData.WeaponDataEx;
            SetPositionAndRotation(bulletSpawnData.Position, bulletSpawnData.Rotation);
            AddForce(bulletSpawnData.Force);
            _currTime = 0;
            _isFree = false;
            _rigid.velocity = Vector3.zero;
            ObjectPoolController.SpawnObject(new PoolObjectParameter(_SpawnParticles, transform.position, transform.rotation));
        }

        private void SetPositionAndRotation(Vector3 position, Vector3 rotation)
        {
            transform.position = position;
            transform.LookAt(rotation);
        }

        protected void Update()
        {
            if (_isFree) return;
            if (_currTime >= _lifeTime)
            {
                Free();
                _isFree = true;
                return;
            }

            _currTime += Time.deltaTime;
        }

        private void ReceiveDamage(DamageReceiver damageReceiver)
        {
            damageReceiver.ReceiveDamage(new DamageData(_weaponDataEx, _weaponDataEx.Data.AttackStat));
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            var receiveDamage = collision.gameObject.GetComponent<IReceiveDamage>();
            if (receiveDamage == null) return;
            if (receiveDamage is DamageReceiver { Owner: ActorBase actorBase })
            {
                if (actorBase == _weaponDataEx.GetOwner) return;
                ReceiveDamage(actorBase.DamageReceiver);
            }
            ObjectPoolController.SpawnObject(new PoolObjectParameter(_CollisionParticles, transform.position, transform.rotation));
            if (_destroyOnCollision)
                Free();
        }

        public override void Free()
        {
            ObjectPoolController.SpawnObject(new PoolObjectParameter(_DestroyParticles, transform.position, transform.rotation));
            base.Free();
        }

        [Button]
        private void AddForce(Vector3 force)
        {
            _rigid.AddForce(force * _rigid.mass);
        }

        public override void Destruct()
        {
            base.Destruct();
            Destroy(gameObject);
        }
    }
}