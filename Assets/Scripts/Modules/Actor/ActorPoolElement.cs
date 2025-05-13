using System;
using Modules.Character;
using UnityEngine;

namespace Modules.Actor
{
    public class ActorPoolElement : PoolableItem
    {
        [SerializeField] private ActorBase _actorBaseRef;
        [SerializeField] private int _id;

        public event Action<ActorPoolElement> OnFreeReady; 

        public ActorBase ActorBaseRef => _actorBaseRef;
        public int Id => _id;
        
        public void Init()
        {
            if(_actorBaseRef)
                _actorBaseRef.Components.Init(_actorBaseRef);
        }

        public void SetActor(ActorBase actorBase)
        {
            _actorBaseRef = actorBase;
            _actorBaseRef.OnDeath += OnDeathHandler;
        }

        private void OnDeathHandler(ActorBase obj)
        {
            OnFreeReady?.Invoke(this);
        }

        public override void Free()
        {
            if (_actorBaseRef)
            {
                _actorBaseRef.ActorFree();
                _actorBaseRef.OnDeath -= OnDeathHandler;
            }
            gameObject.SetActive(false);
            base.Free();
        }

        public void SetId(float id)
        {
            _id = (int)id;
        }
    }
}
