using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Actor.Weapon
{
    [CreateAssetMenu(fileName = "WeaponSettingsData", menuName = "ScriptableData/Weapon/WeaponSettingsData", order = 0)]
    public class WeaponSettingsData : ScriptableObject
    {
        [SerializeField, FoldoutGroup("Spawn Position")] private Vector3 _startPositionOffset;
        [SerializeField, FoldoutGroup("Spawn Position")] private Vector3 _startRotationOffset;

        public Vector3 StartPositionOffset => _startPositionOffset;
        public Vector3 StartRotationOffset => _startRotationOffset;
    }
}
