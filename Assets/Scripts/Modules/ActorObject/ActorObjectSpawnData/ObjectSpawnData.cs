using System;
using UnityEngine;

namespace Modules.ActorObject.ActorObjectSpawnData
{
    [Serializable]
    public class ObjectSpawnData
    {
        public ActorObjectData ActorObjectData;
        public Vector3 Position;
        public Vector3 Rotation;
        
        public ObjectSpawnData(ActorObjectData actorObjectData, Vector3 position, Vector3 rotation)
        {
            ActorObjectData = actorObjectData;
            Position = position;
            Rotation = rotation;
        }

        protected ObjectSpawnData() { }
    }
}
