using System.Collections.Generic;
using System.Linq;
using Core;
using Core.GameEvents;
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
      return actor;
    }

    public ActorBase RespawnCharacter(ActorBase actorBase, Vector3 pos) {
      actorBase.transform.position = GetPositionOnNavMesh(pos);
      actorBase.Data.Revive();
      return actorBase;
    }

    private void OnActorDeathHandler(ActorBase actorBase) {
      HitData hitData = new HitData(actorBase.Data, actorBase.Data.LastDamage);
      _characterBaseEvents.OnActorDeath.Check(actorBase.Data, hitData);
      //_deathHandler.DeathProcess(actorBase);
      DestroyActor(actorBase);
    }

    private Vector3 GetPositionOnNavMesh(Vector3 pos) {
      if (NavMeshHelper.GetPointOnNavMesh(pos, out var hit, 5)) {
        return hit;
      }
      Debug.Log("actor can`t find point on nav mesh");
      return pos;
    }

    public void DestroyActor(ActorBase actor, bool destructData = false) {
      if (actor.Data.IsPlayer) {
        //playerDestroy.Check(null, actor.Data);
        //_player = null;
      }
      //_onActorDestroy.Check(null, actor.Data);
      _actorList.Remove(actor);
      actor.OnDeath -= OnActorDeathHandler;
      //actor.DestroyActor(destructData);
    }

    public void Free()
    {
      OnChangeGameState?.Unsubscribe(null,OnChangeGameStateHandler);
      DestroyAllActors();
    }

    public void DestroyAllActors() {
      for (int i = _actorList.Count - 1; i >= 0; i--) {
        DestroyActor(_actorList[i], true);
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
