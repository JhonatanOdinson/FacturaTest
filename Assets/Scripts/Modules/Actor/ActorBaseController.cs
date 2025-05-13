using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.GameEvents;
using Modules.Actor.Weapon;
using Modules.Character;
using Modules.Tool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Actor {
  public class ActorBaseController : MonoBehaviour {
    [SerializeField, ReadOnly] private List<ActorBase> _actorList = new();
    [SerializeField] private CharacterBaseEvents _characterBaseEvents;
    
    private List<CharacterData> _spawnedList = new();
    public List<ActorBase> GetActors => _actorList;
    public CharacterBaseEvents BaseEvents => _characterBaseEvents;

    public GameEvent OnChangeGameState;
    public event Action<ActorBase> OnCreateActor;
    public event Action<ActorBase> OnDestroyActor;
    
    public ActorBase GetPlayer() {
      return GetActors.FirstOrDefault(e => e.Data.IsPlayer);
    }

    public void Init() {
      OnChangeGameState?.Subscribe(null,OnChangeGameStateHandler);
    }

    private void OnChangeGameStateHandler(object state)
    {
      if (state is GameStateController.GameStateE gameStateE)
      {
        bool actorActiveState = gameStateE == GameStateController.GameStateE.Play;
        _actorList.ForEach(e => e.ActivateActor(actorActiveState));
      }
    }

    public ActorBase CreateCharacter(CharacterDataEx dataEx, Vector3 spawnPos, Transform parent) {
      var actor = Instantiate(dataEx.Data.ActorAsset.LoadFromPrefab(true).Result, parent);
      actor.transform.position = GetPositionOnNavMesh(spawnPos);
      dataEx.SetActor(actor);
      actor.Init(dataEx);
      _actorList.Add(actor);
      actor.OnDeath += OnActorDeathHandler;
      OnCreateActor?.Invoke(actor);
      return actor;
    }

    public ActorBase RespawnCharacter(ActorBase actorBase, Vector3 pos) {
      actorBase.transform.position = GetPositionOnNavMesh(pos);
      actorBase.transform.rotation = Quaternion.Euler(Vector3.zero);
      actorBase.Data.Revive();
      OnCreateActor?.Invoke(actorBase);
      return actorBase;
    }

    private void OnActorDeathHandler(ActorBase actorBase) {
      HitData hitData = new HitData(actorBase.Data, actorBase.Data.LastDamage);
      if (hitData.DamageData.Damager is WeaponDataEx weaponDataEx) {
        if(weaponDataEx.GetOwner.Data != actorBase.Data)
          _characterBaseEvents.OnActorDeath.Check(actorBase.Data, hitData);
      } //else _characterBaseEvents.OnActorDeath.Check(actorBase.Data, hitData);
      
      //DestructActor(actorBase);
    }

    private Vector3 GetPositionOnNavMesh(Vector3 pos) {
      if (NavMeshHelper.GetPointOnNavMesh(pos, out var hit, 5)) {
        return hit;
      }
      return pos;
    }

    public void DestructActor(ActorBase actor) {
      _actorList.Remove(actor);
      //actor.Data.OnDeadEvent -= OnActorDeath;
      OnDestroyActor?.Invoke(actor);
      actor.Destruct();
    }

    public void Free()
    {
      OnChangeGameState?.Unsubscribe(null,OnChangeGameStateHandler);
      DestroyAllActors();
    }

    public void DestroyAllActors() {
      for (int i = _actorList.Count - 1; i >= 0; i--) {
        DestructActor(_actorList[i]);
      }
      ReleaseAllActors();
      //_firstPlayerSpawnList.Clear();
    }

    private void ReleaseAllActors() {
      _spawnedList.ForEach(e => e.ActorAsset.ReleaseAsset());
      _spawnedList.Clear();
    }
  }
}
