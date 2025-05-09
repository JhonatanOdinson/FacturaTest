using Core;
using Modules.Tool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Actor.Weapon
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableData/Weapon/WeaponData", order = 0)]
    public class WeaponData : UniqueId
    {
        [SerializeField,LabelText("Settings"), GUIColor(0.85f, 1f, 1f, 1f), FoldoutGroup("Weapon Main Config"), InlineEditor] 
        private WeaponSettingsData _weaponSettings;
        [SerializeField] private float _attackCooldown = 1.5f;
        [SerializeField] private AssetReferenceInPrefab<WeaponBase> _weaponPrefab;
        [SerializeField] private int _attackStat;
        [SerializeField] private bool _autoFire;
        
        public WeaponSettingsData WeaponSettings => _weaponSettings;
        public float AttackCooldown => _attackCooldown;
        public int AttackStat => _attackStat;
        public bool AutoFire => _autoFire;
        
        public WeaponBase InstantiatePrefab(Transform parent) {
            return Instantiate(_weaponPrefab.LoadFromPrefab(true).Result, parent);
        }
    }
}
