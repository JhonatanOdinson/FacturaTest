using System;
using Modules.Damage;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Modules.Character {
  [Serializable]
  public class HitData {
    [SerializeField] private object _target;
    [SerializeField] private DamageData _damageData;

    public object Target => _target;
    public DamageData DamageData => _damageData;

    public HitData(object target, DamageData damageData) {
      _target = target;
      _damageData = damageData;
    }
  }
}