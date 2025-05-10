using System;
using UnityEngine;

namespace Modules.ActorObject
{
    public class ActorObjectBase : MonoBehaviour
    {
        [SerializeField] private ActorObjectData _data;
        public ActorObjectData Data => _data;
        
        public Action OnDestroy;
        public virtual void Init(object spawnData)
        {
            
        }

        public virtual void Free()
        {
            OnDestroy?.Invoke();
        }

        public virtual void Destruct()
        {
          Destroy(gameObject);
        }

    }
}
