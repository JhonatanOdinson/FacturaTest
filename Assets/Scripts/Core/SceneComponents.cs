//using Library.Scripts.Modules.SpawnManager;

using Modules.Actor;
using Modules.ActorObject;
using Modules.RoadSegmentController;
using UnityEngine;

namespace Core
{
    public class SceneComponents : MonoBehaviour
    {

        [SerializeField] private CameraDirector _cameraDirector;
        [SerializeField] private RoadSegmentController _roadSegmentController;

        [SerializeField] private ActorSpawnController _actorSpawnController;

        [SerializeField] private ActorObjectController _actorObjectController;
       //[SerializeField] private SpawnManager _spawnManager;
       
        //public SpawnManager SpawnManager => _spawnManager;
        public CameraDirector CameraDirector => _cameraDirector;
        public RoadSegmentController RoadSegmentController => _roadSegmentController;
        public ActorSpawnController ActorSpawnController => _actorSpawnController;
        public ActorObjectController ActorObjectController => _actorObjectController;
        
        public void Init()
        {
            _actorSpawnController?.Init();
            _roadSegmentController?.Init();
            _actorObjectController.Init();
            _cameraDirector?.Init();
            
        }

        public void Destruct()
        {
            _cameraDirector?.Destruct();
            _actorSpawnController?.Free();
            _roadSegmentController?.Free();
            _actorObjectController.Free();
            //_spawnManager?.Free();
        }
    }
}
