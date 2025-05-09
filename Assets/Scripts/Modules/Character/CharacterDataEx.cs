using System;
using Modules.Actor.Weapon;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Character {
  [Serializable]
  public class CharacterDataEx {

    [SerializeField] [ReadOnly]
    private string _uniqueId;

    [SerializeField] [ReadOnly]
    private CharacterData _data;

    [SerializeField] private ActorBase _actorBaseRef;
    [SerializeField] private int _maxVitality;
    [SerializeField] private int _currVitality;
    [SerializeField] private int _currAttack;
    [SerializeField] private bool _isDead;
    [SerializeField] private DamageData _lastDamage;
    [ShowInInspector] private WeaponDataEx _weaponDataEx;
    
    private int _receiveDamageCount;
    
    public event Action<int> OnVitalityChange;
    public event Action<CharacterDataEx> OnDeadEvent;

    public string UniqueId => _uniqueId;
    
    public CharacterData Data => _data;
    public ActorBase GetActorRef => _actorBaseRef;
    public int MaxVitality => _maxVitality;
    public int GetCurrVitality => _currVitality;
    public int GetCurrAttack => _currAttack;
    public int ReceiveDamageCount => _receiveDamageCount;
    public bool IsPlayer => _data.IsPlayer;
    public bool IsDead => _isDead;
    public DamageData LastDamage => _lastDamage;
    public WeaponDataEx WeaponDataEx => _weaponDataEx;

    public CharacterDataEx(CharacterData data) {
      _data = data;
      _maxVitality = data.Vitality;
      _currVitality = _maxVitality;
      //_currAttack = data.Damage;
      GenerateID();
      SetWeapon(_data.WeaponData);
    }

    public CharacterDataEx(CharacterDataEx dataEx) {
      _uniqueId = dataEx.UniqueId;
      _data = dataEx.Data;
    }

    public void GenerateID() {
      _uniqueId = Guid.NewGuid().ToString();
    }

    public void SetUniqueId(string uniqueId) {
      _uniqueId = uniqueId;
    }

    public void SetActor(ActorBase actor) {
      _actorBaseRef = actor;
    }

    public void ReceiveDamage(DamageData damageData) {
      if (_isDead) return;
      /*CommonComponents.GetActorBaseController.CharacterBaseEvents.OnBeforeActorDamage.Check(this,
        new HitData(this, damageData));*/
      int damageCount = damageData.Damage > _currVitality ? _currVitality : damageData.Damage;
      _currVitality -= damageCount;
      //_lastDamage = damageData;
      _receiveDamageCount++;
      

      //here creating new DamageData for receiving true damage count (clamped to currVitality)
      /*CommonComponents.GetActorBaseController.CharacterBaseEvents.OnActorDamaged.Check(this,
        new HitData(this, new DamageData(damageData.Damager, damageCount)));*/
      OnVitalityChange?.Invoke(-damageData.Damage);
      if (_currVitality <= 0)
      {
        _isDead = true;
        OnDeadEvent?.Invoke(this);
      }
    }

    public void Revive()
    {
      _isDead = false;
      SetCurrVitality(_maxVitality);
    }

    public void AddCurrVitality(int value, bool callEvent = true) {
      int lastCurrVitality = _currVitality;
      _currVitality = Mathf.Clamp(_currVitality + value, 0, _maxVitality);
      if (callEvent && lastCurrVitality != _currVitality) {
        OnVitalityChange?.Invoke(value);
      }
    }

    public void SetCurrVitality(int value) {
      _currVitality = value;
      OnVitalityChange?.Invoke(0);
    }

    public void SetWeapon(WeaponData weaponData) {
      SetWeapon(weaponData != null ? new WeaponDataEx(weaponData) : null);
    }
    
    public void SetWeapon(WeaponDataEx dataEx) {
      if (_weaponDataEx == dataEx) return;
      _weaponDataEx = dataEx;
    }

    public void Destruct() {
      //_data = null;
    }
  }
}