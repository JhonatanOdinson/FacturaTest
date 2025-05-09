using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Modules.Character {
  [Serializable]
  public class DamageData : Object {
    
    [SerializeField] private object _damager;
    [SerializeField] private int _playerIdSource = -1;
    [SerializeField] private int _damage;
    [SerializeField] private bool _isCritical;
    public int Damage => _damage;
    public int PlayerIdSource => _playerIdSource;
    public object Damager => _damager;
    public bool IsCritical => _isCritical;

    public DamageData(int damage, bool isCritical = false) {
      _damage = damage;
      _isCritical = isCritical;
    }

    public CharacterDataEx GetPlayerFromData() {
      
      
      
      return null;
    }

    public DamageData(object damager, int damage, bool isCritical = false) {
      _damager = damager;
      _damage = damage;
      _isCritical = isCritical;
    }

    public void Add(float value) {
      _damage = Mathf.RoundToInt(_damage+value) ;
    }

    public void Multiplicate(float value) {
      _damage = Mathf.RoundToInt(_damage * value);
    }

    public void Divide(float value) {
      _damage = Mathf.RoundToInt(_damage / value);
    }

    public void SetIsCritical(bool value) {
      _isCritical = value;
    }
  }
}