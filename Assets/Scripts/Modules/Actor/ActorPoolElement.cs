using Modules.Character;
using UnityEngine;

namespace Modules.Actor
{
    public class ActorPoolElement : PoolableItem
    {
        [SerializeField] private ActorBase _actorBaseRef;
        [SerializeField] private int _id;

        public ActorBase ActorBaseRef => _actorBaseRef;
        public int Id => _id;
        
        public void Init()
        {
            
        }

        public void SetActor(ActorBase actorBase)
        {
            _actorBaseRef = actorBase;
        }

        public override void Free()
        {
            
            gameObject.SetActive(false);
            base.Free();
        }

        public void SetId(float id)
        {
            _id = (int)id;
        }
    }
}
