//using Library.Scripts.Modules.SpawnManager;

using Core;
using Modules.Actor;
using Modules.RoadSegmentController;
using UnityEngine;

namespace Library.Scripts.Core
{
    public class SceneComponents : MonoBehaviour
    {

        [SerializeField] private CameraDirector _cameraDirector;
        [SerializeField] private RoadSegmentController _roadSegmentController;

        [SerializeField] private ActorSpawnController _actorSpawnController;
       //[SerializeField] private SpawnManager _spawnManager;
       
        //public SpawnManager SpawnManager => _spawnManager;
        public CameraDirector CameraDirector => _cameraDirector;
        public RoadSegmentController RoadSegmentController => _roadSegmentController;
        public ActorSpawnController ActorSpawnController => _actorSpawnController;
        
        public void Init()
        {
            _roadSegmentController?.Init();
            _actorSpawnController?.Init();
            _cameraDirector?.Init();
            // _spawnManager?.Init();
        }

        public void Destruct()
        {
            _cameraDirector?.Destruct();
            _actorSpawnController?.Free();
            _roadSegmentController?.Free();
            //_spawnManager?.Free();
        }
    }
}
