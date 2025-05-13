using Core.GameEvents;
using Modules.ActorObject;
using Modules.ActorObject.ActorObjectSpawnData;
using Modules.Character;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Modules.Actor.Weapon
{
    public class WeaponRange : WeaponBase
    {
        [SerializeField] private ActorObjectData _bulletData;
       // [SerializeField] private WeaponBullet _bullet;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _bulletForce = 1000f;
        [SerializeField] private GameEvent _onCreateBullet;

        //public WeaponBullet Bullet => _bullet;
        public Transform FirePoint => _firePoint;
        public float BulletForce => _bulletForce;

        [Button]
        public override void Attack(ActorBase target = null)
        {
            base.Attack();
            CreateBullet();
            //_onAttack?.Invoke();
        }

        protected virtual void CreateBullet(/*Vector3 targetPosition, bool checkDistance = true*/)
        {
            /*ObjectPoolController.SpawnObject(new PoolObjectParameter(_bulletCasingParticles, transform.position,
                transform.rotation));*/

            _onCreateBullet.Check(null,
                new BulletSpawnData(
                    _bulletData,
                    _weaponDataEx,
                    _firePoint.position,
                    _firePoint.forward + _firePoint.transform.position, 
                    _firePoint.forward * _bulletForce));

                /*var bullet = Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
                bullet.Init(_weaponDataEx);
                bullet.transform.LookAt(_firePoint.forward + bullet.transform.position);
                bullet.AddForce(_bulletForce, _firePoint);*/
                
        }
    }
}
