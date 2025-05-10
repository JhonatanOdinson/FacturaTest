using Core.GameEvents;
using Modules.ActorObject.ActorObjectSpawnData;
using Modules.Tool.UniversalPool;
using UnityEngine;

namespace Modules.ActorObject
{
    public class ActorObjectController : MonoBehaviour
    {
        [SerializeField] private GameEvent _onSpawnObject;
        [SerializeField] private UniversalPool<ActorObjectElement> _actorObjectPool;
        
        public void Init()
        {
            _onSpawnObject.Subscribe(null,OnTakeObjectHandler);
            _actorObjectPool.Initialize();
            
        }

        private void OnTakeObjectHandler(object obj)
        {
            if (obj is ObjectSpawnData data)
                TakeActor(data);
        }

        private void TakeActor(ObjectSpawnData spawnData)
        {
            if (spawnData is null) return;
            ActorObjectBase objectBase;
            var objectPoolElement = _actorObjectPool.Take();
            objectPoolElement.gameObject.SetActive(true);
            if (objectPoolElement.ActorBaseRef)
            {
                objectPoolElement.Init(objectPoolElement.ActorBaseRef,spawnData);
            }
            else
            {
                objectBase = Instantiate(spawnData.ActorObjectData.ObjectAsset.LoadFromPrefab(true).Result, objectPoolElement.transform);
                objectPoolElement.Init(objectBase,spawnData);
            }
            objectPoolElement.OnReturnObject += ReturnObject;
            //return objectBase;
        }

        private void ReturnObject(ActorObjectElement objectElement)
        {
            objectElement.OnReturnObject -= ReturnObject;
            objectElement.gameObject.SetActive(false);
            _actorObjectPool.Return(objectElement);
        }

        public void Free()
        {
            foreach (var element in _actorObjectPool.GetBusy())
            {
                element.OnReturnObject -= ReturnObject;
            }
            _actorObjectPool.Clear();
        }
        
    }
}
