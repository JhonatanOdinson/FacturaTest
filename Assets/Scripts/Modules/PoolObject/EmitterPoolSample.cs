using System;
using System.Collections.Generic;
using DG.Tweening;
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
     /* if (_audio != null)
        AudioController.Play(_audio, transform);*/

      _isFree = false;
      //InitDamagers();
    }

    /*private void InitDamagers() {
      _damagers.Clear();
      foreach (var damager in GetComponentsInChildren<Damager>(true)) {
        damager.Init(CharDataEx?.GetActorRef);
        _damagers.Add(damager);
      }
    }*/

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
     /* if(_audio != null && _audio.type == AudioData.AudioTypeE.Looped)
        AudioController.Stop(_audio, transform);
      await CommonComponents.ObjectPoolController.ReturnObject(this, instantly);*/
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