using System.Collections.Generic;
using Core;
using Modules.Character;
using UnityEngine;
using UnityEngine.AI;

namespace Modules.Actor.ActorComponent {
  public class ActorAI : ActorComponentBase {
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private PlayMakerFSM _aiFsm;

    public NavMeshAgent NavMeshAgent => _navMeshAgent;

    public override void Init(ActorBase actorBase) {
      base.Init(actorBase);
      ActorOwner.Data.OnDeadEvent += OnDeath;
    }

    public override void SetEnabled(bool state)
    {
      base.SetEnabled(state);
      EnableFsm(state);
    }

    /*public void SetFsmTemplate(FsmTemplate template, bool callStart = true) {
      if (_aiFsm == null || _aiFsm.FsmTemplate == template || ActorOwner.Data.IsDead) return;
      if (_aiFsm.FsmTemplate != null) {
        _aiFsm.SendEvent(GlobalEvents.ResetFSM);
        _aiFsm.Fsm.Update();
      }
      _aiFsm.SetFsmTemplate(template);
      if(IsEnable)
        _aiFsm.Fsm.Start();
    }*/

    private void OnDeath(CharacterDataEx dataEx) {
      _aiFsm.SendEvent(GlobalEvents.ResetFSM);
      _aiFsm.enabled = false;
    }

    public void EnableFsm(bool state) {
      Debug.Log($"EnableFsm: {state}");
      if(!state) _aiFsm.SendEvent(GlobalEvents.ResetFSM);
      _aiFsm.enabled = state;
    }

    public override void Destruct() {
      base.Destruct();
      ActorOwner.Data.OnDeadEvent -= OnDeath;
    }
  }
}