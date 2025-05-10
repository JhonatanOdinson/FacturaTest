using System;
using Modules.Character;
using Modules.PoolObject;
using UnityEngine;

namespace Modules.Emitters {
  public class PoolBase : MonoBehaviour {
    protected CharacterDataEx CharDataEx;
    private float _lifeTime;
    private Transform _followTransform;
    private bool _alignToRb;
    private Rigidbody _alignRb;
    private bool _hasStarted = false;
    private Action _onFinish;
    private PoolDataBase _data;
    public PoolDataBase Data => _data;

    public float LifeTime {
      get => _lifeTime;
      set => _lifeTime = value;
    }

    public Transform FollowTransform {
      get => _followTransform;
      set => _followTransform = value;
    }

    public bool IsFollowRotation { get; set; }

    public void OnFinish() => _onFinish?.Invoke();

    public void SetParameter(PoolObjectParameter param) {
      if (transform != null) {
        var tr = transform;
        tr.position = param.Pos;
        tr.rotation = param.Rotation;  
      }
      
      CharDataEx = param.CharacterDataEx;
      _lifeTime = param.LifeTime;
      _onFinish = param.OnFinish;
      _data = param.PoolData;
    }

    public virtual void ActivateObj() {
      gameObject.SetActive(true);
    }
    
    public virtual void DeactivateObj() {
      gameObject.SetActive(false);
      _hasStarted = false;
    }
    
    private void Update() {
      if (!_hasStarted) {
        OnStart();
        _hasStarted = true;
      }
      
      OnUpdate();
      UpdateTransform();
      UpdateRotation();
      AlignToRigidbody();
    }
    
    public virtual void OnStart(){}
    public virtual void OnUpdate(){}
    
    public virtual void ScaleToZero(float dur = 1){}

    private void UpdateTransform() {
      if (_followTransform != null) {
        transform.position = _followTransform.position;
      }
    }

    private void UpdateRotation() {
      if (!IsFollowRotation || _followTransform == null) return;
      transform.rotation = _followTransform.rotation;
    }

    private void AlignToRigidbody() {
      if(!_alignToRb || _alignRb == null) return;
      var velocity = _alignRb.transform.forward; //test for take forward, because velocity is not always accurate
      transform.rotation = Quaternion.FromToRotation(transform.forward, velocity) * transform.rotation;
    }

    public void ActivateAlign(Rigidbody rb) {
      _alignRb = rb;
      _alignToRb = true;
    }

    public void ClearTransform() {
      _followTransform = null;
      _alignToRb = false;
      _alignRb = null;
    }
    
    public bool CheckTransform(Transform tr) {
      return FollowTransform == tr;
    }
  }
}
