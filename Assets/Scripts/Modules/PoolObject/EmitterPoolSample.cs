using System;
using System.Collections.Generic;
using DG.Tweening;
using Library.Scripts.Core;
using Modules.Actor;
using Modules.Actor.Components;
using Modules.Emitters;
using UnityEngine;

namespace Modules.PoolObject {
  [Serializable]
  public class EmitterPoolSample : PoolBase {
    [SerializeField] protected ParticleSystem _emmiterRef;
    //[SerializeField] private AudioData _audio;

    private List<Damager> _damagers = new();

    private Vector3 _offset;

    protected bool _isFree = true;

    public ParticleSystem EmitterRef => _emmiterRef;

    public void Init() {
      //InitDamagers();
    }

    public override void ActivateObj() {
      base.ActivateObj();
      _isFree = false;
    }

    private void UpdateToParams() {
      CheckStopEmitterType();
    }

    public override void OnUpdate() {
      UpdateToParams();
    }

    private void CheckStopEmitterType() {
      if (_emmiterRef != null) {
        if (!_emmiterRef.IsAlive()) {
          FreeEmitter();
        }
      }
    }
    
    public override void ScaleToZero(float duration = 1) {
      ClearTransform();
      transform.DOScale(Vector3.zero, duration).OnComplete(() => {
        transform.localScale = Vector3.one;
        if(!_isFree)
          FreeEmitter();
      });
    }
    
    public virtual async void FreeEmitter(bool instantly = false) {
      if(_isFree) return;
      _isFree = true;
      CharDataEx = null;
      await CommonComponents.ObjectPoolController.ReturnObject(this, instantly);
      ClearTransform();
      FreeDamagers();
    }

    private void FreeDamagers() {
      _damagers.ForEach(e => e.FreeOwner());
    }

    private void OnDestroy() {
      DOTween.Kill(transform);
    }
  }
}