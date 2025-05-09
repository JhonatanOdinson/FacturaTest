using System;
using Modules.Actor;
using Modules.Actor.Components;
using UnityEngine;

namespace Modules.Character
{
    public class ActorBase : MonoBehaviour
    {
        [SerializeField] protected CharacterDataEx _charDataEx;
        [SerializeField] private ActorComponents _actorComponents;
        [SerializeField] private DamageReceiver _damageReceiver;
        
        private ActorBase _currentTarget;
        
        public CharacterDataEx Data => _charDataEx;
        public ActorComponents Components => _actorComponents;
        public DamageReceiver DamageReceiver => _damageReceiver;
        public ActorBase CurrentTarget => _currentTarget;
        
        public event Action<ActorBase> OnDeath;
        public Action<ActorBase, ActorBase> OnTargetChange;
        public event Action OnDestruct;
        
        public virtual void Init(CharacterDataEx dataEx) {
            _charDataEx = dataEx;
            _actorComponents.Init(this);
            Subscribe();
            _charDataEx.SetActor(this);
            //_audioCreatureInstance = new AudioCreatureInstance(this);
        }
        
        public void SetCurrentTarget(ActorBase target) {
            var prevTarget = _currentTarget;
            _currentTarget = target;
            OnTargetChange?.Invoke(prevTarget, _currentTarget);
        }
        
        public void Update() {
            _actorComponents.UpdateExecute();
        }

        public void FixedUpdate() {
            _actorComponents.FixedUpdateExecute(Time.fixedDeltaTime);
        }
        
        private void ReceiveDamage(DamageData damageData) {
            if(damageData.Damager is CharacterDataEx charDataEx && charDataEx == Data) return;
            CharacterDataController.ReceiveDamage(_charDataEx, damageData);
        }
        
        private void Subscribe() {
            _charDataEx.OnDeadEvent += OnDeadHandler;
        }

        private void OnDeadHandler(CharacterDataEx dataEx)
        {
            if(dataEx.IsDead)
                Death();
        }

        private void Death()
        {
            OnDeath?.Invoke(this);
        }

        private void Unsubscribe() {
            _charDataEx.OnDeadEvent -= OnDeadHandler;
        }
        
        public virtual void Destruct() {
            Unsubscribe();
            //_charDataEx.AbilityDataManager.OnDestructActor();
            //  _audioCreatureInstance.Destruct();
            _actorComponents.Destruct();
            _charDataEx.Destruct();
   
            _damageReceiver.OnReceiveDamage -= ReceiveDamage;
            Destroy(gameObject);
        }
    }
}
