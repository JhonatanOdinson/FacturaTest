using System;
using Modules.Actor.Weapon;
using UnityEngine;

namespace Modules.ActorObject.ActorObjectSpawnData
{
    [Serializable]
    public class BulletSpawnData: ObjectSpawnData
    {
        public WeaponDataEx WeaponDataEx;
        public Vector3 Force;

        public BulletSpawnData(ActorObjectData actorObjectData, WeaponDataEx weaponDataEx, Vector3 position, Vector3 rotation, Vector3 force)
        {
            ActorObjectData = actorObjectData;
            WeaponDataEx = weaponDataEx;
            Position = position;
            Rotation = rotation;
            Force = force;
        }
    }
}
