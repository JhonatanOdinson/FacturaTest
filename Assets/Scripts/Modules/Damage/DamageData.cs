using System;
using Modules.Character;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Modules.Damage {
  [Serializable]
  public class DamageData : Object {
    
    [SerializeField] private object _damager;
    [SerializeField] private int _damage;
    public int Damage => _damage;
    public object Damager => _damager;

    public DamageData(int damage) {
      _damage = damage;
    }
    
    public DamageData(object damager, int damage) {
      _damager = damager;
      _damage = damage;
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
  }
}