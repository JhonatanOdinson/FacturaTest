using System;
using Modules.ActorObject.ActorObjectSpawnData;
using UnityEngine;

namespace Modules.ActorObject
{
    public class ActorObjectElement : PoolableItem
    {
        [SerializeField] private ActorObjectBase _actorObjectRef;
        public ActorObjectBase ActorBaseRef => _actorObjectRef;
        public event Action<ActorObjectElement> OnReturnObject; 

        public void Init(ActorObjectBase actorObject, ObjectSpawnData objectSpawnData)
        {
            _actorObjectRef = actorObject;
            _actorObjectRef.OnDestroy += OnDestroyHandler;
            _actorObjectRef.Init(objectSpawnData);
        }

        private void OnDestroyHandler()
        {
            OnReturnObject?.Invoke(this);
        }

        public override void Free()
        {
            _actorObjectRef.OnDestroy -= OnDestroyHandler;
            gameObject.SetActive(false);
            base.Free();
        }
    }
}
