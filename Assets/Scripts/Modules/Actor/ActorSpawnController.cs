using System.Collections.Generic;
using System.Linq;
using Core;
using Core.GameEvents;
using Library.Scripts.Core;
using Modules.Character;
using Modules.ScriptableObjects;
using Modules.Tool.UniversalPool;
using Unity.AI.Navigation;
using UnityEngine;

namespace Modules.Actor
{
    public class ActorSpawnController : MonoBehaviour
    {
        [SerializeField] private ScenarioData _scenarioData;
        [SerializeField] private GameEvent _onTakeActor;
        [SerializeField] private GameEvent _onFreeActor;
        [SerializeField] private GameEvent _onChangeGameState;
        [SerializeField] private UniversalPool<ActorPoolElement> _actorPool;

        private ActorPoolElement _player;
        private GameStateController.GameStateE _currentGameState;

        public void Init()
        {
            _actorPool.Initialize();
            _scenarioData = GameDirector.GetGameConfig.GameScenario;
            _onTakeActor?.Subscribe(this,OnTakeActorHandler);
            _onFreeActor?.Subscribe(this,OnFreeActorHandler);
            _onChangeGameState?.Subscribe(this,OnChangeGameStateHandler);
            PlacePlayer();
        }

        private void OnChangeGameStateHandler(object gameState)
        {
            if (gameState is GameStateController.GameStateE gameStateE)
            {
                _currentGameState = gameStateE;
                if(gameStateE == GameStateController.GameStateE.None)
                    Restart();
            }
        }

        public void Restart()
        {
            List<ActorPoolElement> actorList = _actorPool.GetBusy().ToList();
            for(int i = 0;i<actorList.Count;i++)
            {
                if (!actorList[i].ActorBaseRef.Data.IsPlayer)
                    FreeActor(actorList[i]);
            }

            PlacePlayer();
        }

        private void PlacePlayer()
        {
            ActorBase actorBase = TakeActor(_scenarioData.Player,Vector3.zero);
            actorBase.ActivateActor(false);
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
            actorElement.OnFreeReady -= FreeActor;
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
            var actorPoolElement = GetActorElement(characterData);
            if (actorPoolElement.ActorBaseRef)
            {
                actorBase = CommonComponents.ActorBaseController.RespawnCharacter(actorPoolElement.ActorBaseRef,
                    placeTransformPosition);
            }
            else
            {
                actorBase = CommonComponents.ActorBaseController.CreateCharacter(
                    new CharacterDataEx(characterData), placeTransformPosition, actorPoolElement.transform);
            }

            actorPoolElement.SetActor(actorBase);
            
            if(_currentGameState == GameStateController.GameStateE.Play)
                actorBase.ActivateActor(true);
            
            actorPoolElement.OnFreeReady += FreeActor;
            actorPoolElement.gameObject.SetActive(true);
            actorPoolElement.SetId(id);
            return actorBase;
        }

        private ActorPoolElement GetActorElement(CharacterData characterData)
        {
            ActorPoolElement actorPoolElement = null;
            if (characterData.IsPlayer)
            {
                if (_player)
                {
                    actorPoolElement = _player;
                }
                else
                {
                    actorPoolElement = _actorPool.Take();
                    _player = actorPoolElement;
                }
            }
            else
            {
                actorPoolElement = _actorPool.Take();
            }

            return actorPoolElement;
        }


        public void Free()
        {
            _onTakeActor?.Unsubscribe(this,OnTakeActorHandler);
            _onFreeActor?.Unsubscribe(this,OnFreeActorHandler);
            _onChangeGameState?.Unsubscribe(this,OnChangeGameStateHandler);
            _actorPool.Clear();
        }
    }
}
