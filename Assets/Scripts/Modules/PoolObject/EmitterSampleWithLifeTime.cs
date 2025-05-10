using Modules.PoolObject;
using UnityEngine;

namespace Modules.Emitters {
  public class EmitterSampleWithLifeTime : EmitterPoolSample {
    private float _curTime;
    private bool _isStop;

    public override void ActivateObj() {
      base.ActivateObj();
      var main = _emmiterRef.main;
      main.stopAction = ParticleSystemStopAction.Callback;
      _curTime = 0;
      _isStop = false;
    }

    public override void OnUpdate() {
      if (_isStop) return;
      if (_curTime < LifeTime) _curTime += Time.deltaTime;
      else {
        StopEmitter();
      }
    }

    private void StopEmitter() {
      _isStop = true;
      _emmiterRef.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    private void OnParticleSystemStopped() {
      FreeEmitter();
    }
  }
}