using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Modules.Actor.Weapon
{
    public class WeaponRange : WeaponBase
    {
        [SerializeField] private WeaponBullet _bullet;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _bulletForce = 1000f;
        [SerializeField] protected UnityEvent _onAttack;

        public WeaponBullet Bullet => _bullet;
        public Transform FirePoint => _firePoint;
        public float BulletForce => _bulletForce;

        [Button]
        public override void Attack(/*Transform target*/)
        {
            base.Attack(/*target*/);
            CreateBullet(/*target.position*/);
            _onAttack?.Invoke();
        }

        protected virtual void CreateBullet(/*Vector3 targetPosition, bool checkDistance = true*/)
        {
            /*ObjectPoolController.SpawnObject(new PoolObjectParameter(_bulletCasingParticles, transform.position,
                transform.rotation));*/


                var bullet = Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
                bullet.Init(_weaponDataEx);
                bullet.transform.LookAt(_firePoint.forward + bullet.transform.position);
                bullet.AddForce(_bulletForce, _firePoint);
                
        }
    }
}
