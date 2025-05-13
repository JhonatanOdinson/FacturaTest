using Core;
using Modules.Actor.Weapon;
using Modules.Tool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Character {
  [CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableData/Character/CharacterData", order = 0)]
  public class CharacterData : UniqueId
  {
      [SerializeField] private bool _isPlayer;
      [SerializeField] private int _vitality;
      [SerializeField] private int _reward;
      [SerializeField] private int _speed;
      [SerializeField] private WeaponData _weaponData;
      [SerializeField] private AssetReferenceInPrefab<ActorBase> _actorAsset;

      public bool IsPlayer => _isPlayer;
      public int Vitality => _vitality;
      public int Reward => _reward;
      public float Speed => _speed;
      public AssetReferenceInPrefab<ActorBase> ActorAsset => _actorAsset;
      public WeaponData WeaponData => _weaponData;
  }
}