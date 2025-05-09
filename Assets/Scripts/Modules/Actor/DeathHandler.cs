using System;
using Modules.Character;
using Modules.Tool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Actor {
  [Serializable]
  public class DeathHandler {
    /*[SerializeField] private PoolDataBase _coinEmmitter;
    [SerializeField] private PoolDataBase _deathSplash;

    [BoxGroup("DeathDecal")]
    [SerializeField] private DecalPoolData _deathSpot;
    [BoxGroup("DeathDecal")]
    [SerializeField] private LayerMask _layer;
    [BoxGroup("DeathDecal")]
    [SerializeField] private float _offsetY = 0.03f;

    public void Init() {
    }

    public void DeathProcess(ActorBase actor) {
      FindAltar(actor);
      CheckSoulSpawn(actor);
      SpawnDeathDecal(actor);
    }

    private void FindAltar(ActorBase actor) {
      if (actor == null || actor.Data.IsPlayer || actor.Data.IsInDungeon) return;
      var playerDamager = GetPlayerDamager(actor.Data.LastDamage, actor);
      UobjectAltar altar = null;
      var uobjList = CommonComponents.GetUobjectController.GetUobjectList;
      foreach (var t in uobjList) {
        if (t is UobjectAltar cur && cur.GetPlayerInRadius() &&
            cur.GetActorInRadius(actor)) altar = cur;
      }

      int coinCount = actor.Data.Coins * (playerDamager != null
        ? CommonComponents.PlayerDataManager.GetPlayerDataController(playerDamager.PlayerId).CoinsDropMod
        : 1);
      //TO altar
      if (altar != null) {
        SpawnCoinEmitter(actor, altar.transform, coinCount, () => { altar.AddCoins(coinCount); });
      }
      //To player
      else {
        SpawnCoinEmitterForAllPlayers(actor, coinCount);
      }

      int chargeCount = 1;
      var damager = actor.Data.LastDamage.Damager;
      if (damager is SpellDataEx spellDataEx) {
        //Debug.LogError($"Killed actors is {spellDataEx.KilledActors}");
        chargeCount += spellDataEx.KilledActors;
      }
      if(playerDamager != null)
        CommonComponents.PlayerDataManager.GetPlayerDataController(playerDamager.PlayerId).Broom.AbilityDataEx.AddChargeCount(chargeCount);
    }
    
    private void CheckSoulSpawn(ActorBase actor) {
      if(actor.Data.IsInDungeon) return;
      var itemDrops = GameDirector.GetGameConfig.DeathDropManager.CheckCanSpawn(actor.Data.GetData);
      if (!actor.Data.IsPlayer && itemDrops.Count > 0) {
        foreach (var itemDrop in itemDrops) {
          for (int i = 0; i < itemDrop.Count; i++) {
            NavMeshHelper.GetPointOnNavmeshLargeRadius(actor.transform.position + itemDrop.GetSpawnOffset(),
              out var spawnPos, 5, 50, 1 << 0 | 1 << 4);
            if (itemDrop.SpawnBlueprints) {
              var blueprint = CommonComponents.SpellController.GetBlueprint();
              if(blueprint == null) break;
              CommonComponents.GetUobjectController.SpawnBlueprint(blueprint, spawnPos);
            }
            else {
              var uobj = CommonComponents.GetUobjectController.SpawnUobject(itemDrop.DropData, spawnPos);
              if (uobj is UobjectCompanionRevive revive) {
                revive.SetReviveData(actor.Data);
              }
              uobj.SetPosition(spawnPos, true);
              if(uobj is UobjectSoul soul)
                soul.SpawnEmitter(actor);
            }
          }
        }
      }
    }

    private void SpawnCoinEmitterForAllPlayers(ActorBase fromActor, int coinsCount) {
      var alivePlayers = CommonComponents.PlayerDataManager.GetAllPlayerDataControllers()
        .FindAll(e => !e.PlayerDataEx.IsDead);
      int coinsPerPlayer = Mathf.CeilToInt((float)coinsCount / alivePlayers.Count);
      foreach (var controller in alivePlayers) {
        SpawnCoinEmitter(fromActor, controller.PlayerDataEx.GetActorRef.transform, coinsPerPlayer, () => controller.ModifyCoin(coinsPerPlayer));
      }
    }

    private void SpawnCoinEmitter(ActorBase actor, Transform tr, int coinsCount, Action onFinish) {
      var position = actor.Components.GetPositionMarkers.GetCenterPoint();
      ObjectPoolController.SpawnObject(new PoolObjectParameter(_deathSplash, pos: position));
      ObjectPoolController.SpawnObject(new PoolObjectParameter(_coinEmmitter,
        tr: actor.Components.GetPositionMarkers.GetCenterTransform(),
        pos: position, forceFieldTr: tr, count: coinsCount, onFinish: onFinish));
    }

    private void SpawnDeathDecal(ActorBase actor) {
      if (actor.Data.IsPlayer) return;
      Vector3 castPos = actor.transform.position + Vector3.up * 2;
      RaycastHit[] hits = Physics.RaycastAll(castPos, -Vector3.up, 20,_layer);
      foreach (var t in hits) {
        if (t.collider == null) return;
        ObjectPoolController.SpawnObject(new PoolObjectParameter(_deathSpot,
          t.point + new Vector3(0, _offsetY, 0), Quaternion.LookRotation(t.normal)));
      }
    }

    private CharacterDataEx GetPlayerDamager(DamageData damageData, ActorBase target) {
      if (!LocalMultiplayerController.IsLocalMultiplayer) return GameDirector.GetPlayerDataEx(1);
      
      if (damageData.Damager is SpellDataEx spell) {
        CharacterDataEx character = (CharacterDataEx) spell.Owner;
        return CheckForCompanion(character);
      }
      if (damageData.Damager is PerkDataEx perkEx) {
        CharacterDataEx character = (CharacterDataEx) perkEx.Creator;
        return CheckForCompanion(character);
      }
      if (damageData.Damager is PerkData) {
        return GetNearestPlayer(target.transform.position).Data;
      }
      if (damageData.Damager is UOData) {
        return GetNearestPlayer(target.transform.position).Data;
      }
      if (damageData.Damager is CharacterDataEx dataEx) {
        return CheckForCompanion(dataEx);
      }
      if (damageData.Damager is BroomEventData broomEventData) {
        return broomEventData.Owner;
      }

      return GetNearestPlayer(target.transform.position).Data;
    }

    //check if damager is player or companion, if companion - then get player owner of this companion
    private CharacterDataEx CheckForCompanion(CharacterDataEx damager) {
      if (damager.IsPlayer) return damager;
      if (damager.GetLoyalty == CharacterDataEx.LoyaltyE.Enemy)
        return damager.GetActorRef != null ? GetNearestPlayer(damager.GetActorRef.transform.position).Data : GameDirector.GetPlayerDataEx(1);
      //find in Companions list and in DeadCompanions because companion might be dead but companion`s spell can kill someone
      return CommonComponents.PlayerDataManager.GetAllPlayerDataControllers().Find(e =>
          e.Companions.Contains(damager))?.PlayerDataEx;
    }

    public ActorBase GetNearestPlayer(Vector3 pos) {
      var players = CommonComponents.GetActorBaseController.GetPlayers();
      if (players.Count == 0) return null;
      if (players.Count == 1) return players[0];
      ActorBase nearestPlayer = null;
      for (int i = 0; i < players.Count; i++) {
        if (nearestPlayer == null ||
            (nearestPlayer.transform.position - pos).magnitude > (players[i].transform.position - pos).magnitude)
          nearestPlayer = players[i];
      }

      return nearestPlayer;
    }
    */
  }
}