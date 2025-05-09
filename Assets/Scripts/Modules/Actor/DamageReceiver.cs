using System;
using Modules.Character;
using Modules.Damage;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Modules.Actor.Components {
  public class DamageReceiver : MonoBehaviour, IReceiveDamage {
    [SerializeField] private UnityEvent _onReceiveDamage;
    [SerializeField] private bool _useDamageMultiplier = false;
    [SerializeField, ShowIf("_useDamageMultiplier")] private float _damageMultiplier = 1f;
    public Action<DamageData> OnReceiveDamage;

    private object _owner;
    public object Owner => _owner;

    public void Init(object owner) {
      _owner = owner;
    }

    public void ReceiveDamage(DamageData damageData) {
      if (_useDamageMultiplier && damageData.Damage > 0 ) {
        int newDamageCount = Mathf.RoundToInt(Mathf.Clamp(damageData.Damage * _damageMultiplier, 1, int.MaxValue));
        damageData.Add(newDamageCount - damageData.Damage);
      }
      OnReceiveDamage?.Invoke(damageData);
      _onReceiveDamage?.Invoke();
    }
  }
}