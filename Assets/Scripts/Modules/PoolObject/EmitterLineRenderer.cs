using Modules.Emitters;
using UnityEngine;

namespace Modules.PoolObject {
  public class EmitterLineRenderer : EmitterPoolSample {
    [SerializeField] private LineRenderer _lineRenderer;

    private Transform _ownerFollow;
    private Transform _targetFollow;
    private float _currTime;

    public void SetOwnerAndTargetTr(Transform owner, Transform target) {
      _ownerFollow = owner;
      _targetFollow = target;
    }

    public void SetLifeTime(float lifeTime) {
      LifeTime = lifeTime;
    }

    public override void OnUpdate() {
      base.OnUpdate();
      UpdateToParams();
      _currTime += Time.deltaTime;
      if(_currTime >= LifeTime)
        FreeEmitter();
    }

    private void UpdateToParams() {
      if (_targetFollow == null) return;
      _lineRenderer.SetPosition(0, _ownerFollow.position);
      _lineRenderer.SetPosition(1, _targetFollow.position);
    }

    public override void DeactivateObj() {
      base.DeactivateObj();
      _targetFollow = null;
      _currTime = 0f;
    }
  }
}