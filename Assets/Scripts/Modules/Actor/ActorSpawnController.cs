using System.Collections.Generic;
using System.Linq;
using Core.GameEvents;
using Library.Scripts.Core;
using Modules.Character;
using Modules.ScriptableObjects;
using Modules.Tool.UniversalPool;
using Unity.AI.Navigation;
using Unity.Burst.Intrinsics;
using UnityEngine;

namespace Modules.Actor
{
    public class ActorSpawnController : MonoBehaviour
    {
        [SerializeField] private ScenarioData _scenarioData;
        [SerializeField] private GameEvent _onTakeActor;
        [SerializeField] private GameEvent _onFreeActor;
        [SerializeField] private UniversalPool<ActorPoolElement> _actorPool;

        public void Init()
        {
            _actorPool.Initialize();
            _scenarioData = GameDirector.GetGameConfig.GameScenario;
            _onTakeActor?.Subscribe(null,OnTakeActorHandler);
            _onFreeActor?.Subscribe(null,OnFreeActorHandler);
            PlacePlayer();
        }

        private void PlacePlayer()
        {
            ActorBase actorBase = TakeActor(_scenarioData.Player,Vector3.zero);
            actorBase.StopActor(true);
        }

        private void OnFreeActorHandler(object actorId)
        {
            if (actorId is int id)
            {
                List<ActorPoolElement> busyActorList =  _actorPool.GetBusy().ToList();
                List<ActorPoolElement> freeActorList = busyActorList.FindAll(e => e.Id == id && !e.ActorBaseRef.Data.IsPlayer);
                foreach (var actorElement in freeActorList)
                {
                    FreeActor(actorElement);
                }
            }
        }

        private void FreeActor(ActorPoolElement actorElement)
        {
            _actorPool.Return(actorElement);
        }

        private void OnTakeActorHandler(object obj)
        {
            if (obj is NavMeshSurface navMeshSurface)
            {
                int takeActorCount = Random.Range(_scenarioData.EnemyPerSegment.x, _scenarioData.EnemyPerSegment.y);
                for (int i = 0; i < takeActorCount; i++)
                {
                    Vector3 navmeshPos = navMeshSurface.gameObject.transform.position;
                    TakeActor(_scenarioData.EnemyList[Random.Range(0, _scenarioData.EnemyList.Count)],
                        GetRandomPosition(navMeshSurface.size) + navmeshPos,
                        (int)navmeshPos.z);
                }
            }
        }

        private Vector3 GetRandomPosition(Vector3 size)
        {
            float halfX = size.x / 2;
            float halfZ = size.z / 2;
            return new Vector3(Random.Range(-halfX, halfX), 0, Random.Range(-halfZ, halfZ));
        }

        private ActorBase TakeActor(CharacterData characterData, Vector3 placeTransformPosition, int id = 0)
        {
            ActorBase actorBase;
            var actorPoolElement = _actorPool.Take();
            if (actorPoolElement.ActorBaseRef)
            {
                actorBase = CommonComponents.ActorBaseController.RespawnCharacter(actorPoolElement.ActorBaseRef,
                    placeTransformPosition);
            }
            else
            {
                actorPoolElement.Init();
                actorBase = CommonComponents.ActorBaseController.CreateCharacter(
                    new CharacterDataEx(characterData), placeTransformPosition, actorPoolElement.transform);
                actorPoolElement.SetActor(actorBase);
            }

            actorPoolElement.SetId(id);
            actorPoolElement.gameObject.SetActive(true);
            return actorBase;
        }


        public void Free()
        {
            _onTakeActor?.Unsubscribe(null,OnTakeActorHandler);
            _onFreeActor?.Unsubscribe(null,OnFreeActorHandler);
            _actorPool.Clear();
        }
    }
}
