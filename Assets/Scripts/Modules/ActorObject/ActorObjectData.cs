using Modules.Actor.Weapon;
using Modules.Character;
using Modules.Tool;
using UnityEngine;

namespace Modules.ActorObject
{
    [CreateAssetMenu(fileName = "ActorObjectData", menuName = "ScriptableData/ActorObject/ActorObjectData", order = 0)]
    public class ActorObjectData : ScriptableObject
    {
        [SerializeField] private AssetReferenceInPrefab<ActorObjectBase> _objectAsset;
        
        public AssetReferenceInPrefab<ActorObjectBase> ObjectAsset => _objectAsset;
    }
}
