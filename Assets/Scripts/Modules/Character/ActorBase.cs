using System;
using Modules.Actor;
using Modules.Actor.Components;
using Modules.Actor.Weapon;
using Modules.Damage;
using Modules.Emitters;
using Modules.PoolObject;
using UnityEngine;

namespace Modules.Character
{
    public class ActorBase : MonoBehaviour
    {
        [SerializeField] protected CharacterDataEx _charDataEx;
        [SerializeField] private ActorComponents _actorComponents;
        [SerializeField] private DamageReceiver _damageReceiver;
        [SerializeField] private Transform _healthBarAnchor;
        
        [SerializeField] private PoolDataBase _deadParticles;

        public CharacterDataEx Data => _charDataEx;
        public ActorComponents Components => _actorComponents;
        public DamageReceiver DamageReceiver => _damageReceiver;
        public Transform HealthBarAnchor => _healthBarAnchor;

        public event Action<ActorBase> OnDeath;
        public event Action<ActorBase> OnFree;
        public Action<ActorBase, ActorBase> OnTargetChange;
        public event Action OnDestruct;
        
        public virtual void Init(CharacterDataEx dataEx) {
            _charDataEx = dataEx;
            _actorComponents.Init(this);
            Subscribe();
            _charDataEx.SetActor(this);
            _damageReceiver.Init(this);
            _damageReceiver.OnReceiveDamage -= ReceiveDamage;
            _damageReceiver.OnReceiveDamage += ReceiveDamage;
        }

        public void Attack(ActorBase target) {
            _actorComponents.FetchComponent<WeaponManager>().Attack(target);
        }
        
        public void Update() {
            _actorComponents.UpdateExecute();
        }

        public void FixedUpdate() {
            _actorComponents.FixedUpdateExecute();
        }
        
        private void ReceiveDamage(DamageData damageData) {
            _charDataEx.ReceiveDamage(damageData);
        }
        
        private void Subscribe() {
            _charDataEx.OnDeadEvent += OnDeadHandler;
        }

        private void OnDeadHandler(CharacterDataEx dataEx)
        {
            if (dataEx.IsDead)
            {
                _damageReceiver.OnReceiveDamage -= ReceiveDamage;
                Unsubscribe();
                Death();
            }
                
        }

        public void ActivateActor(bool state)
        {
            _actorComponents.UpdateEnableComponents(state);
        }
        
        private void Death()
        {
            ObjectPoolController.SpawnObject(new PoolObjectParameter(_deadParticles, transform.position, transform.rotation));
            OnDeath?.Invoke(this);
        }

        private void Unsubscribe() {
            _charDataEx.OnDeadEvent -= OnDeadHandler;
        }
        
        public virtual void Destruct() {
            Unsubscribe();
            _actorComponents.Destruct();
            _charDataEx.Destruct();
   
            _damageReceiver.OnReceiveDamage -= ReceiveDamage;
            Destroy(gameObject);
        }

        public void ActorFree()
        {
            OnFree?.Invoke(this);
            ActivateActor(false);
        }
    }
}
