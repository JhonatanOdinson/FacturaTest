using System;
using System.Threading.Tasks;
using Cinemachine;
using Library.Scripts.Core;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Core
{
    public class CameraDirector : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineVirtualCamera _targetCamera;
        [SerializeField] private MiniTransform Offset;
        [SerializeField] private float smoothTime = 0.24f;
        [SerializeField] private float maxSpeed = 100;
       
        private GameStateController _gameStateController;
        private bool _isAttached;
        private Transform _target;
        
        public Camera Camera => _camera;

        public void Init() {
            //var cameraData = GetCameraRef.GetUniversalAdditionalCameraData();
            //cameraData.cameraStack.Add(CommonComponents.Instance.UiCanvasRef.UiCameraRef);
            //AddCameraStack(CommonComponents.Instance.UiCanvasRef.UiCameraRef);
            //CommonComponents.ActorBaseController.BaseEvents.OnActorDeath.Subscribe(null, UnAttach);
            _gameStateController = CommonComponents.GameStateController;
            _gameStateController.OnStateChange += OnStateChange;
        }
        
        public void AddCameraStack(Camera cam) {
            var cameraData = _camera.GetUniversalAdditionalCameraData();
            cameraData.cameraStack.Add(cam);
        }

        public void RemoveCameraFromStack(Camera cam) {
            var cameraData = _camera.GetUniversalAdditionalCameraData();
            cameraData.cameraStack.Remove(cam);
        }
        
        private void OnStateChange(GameStateController.GameStateE gameStateE) {
            if (gameStateE == GameStateController.GameStateE.Idle)
            {
                AttachToActor(CommonComponents.ActorBaseController.GetPlayer().transform);
            }
            else if (gameStateE == GameStateController.GameStateE.None)
            {
                UnAttach();
            }
        }
        
        private Vector3 _camVelocity;
        public void AttachToActor(Transform target)
        {
            _target = target;
            _targetCamera.Follow = target; 
            _targetCamera.LookAt = target;

            _targetCamera.gameObject.SetActive(true);
            _isAttached = true;
        }
        
        public void UnAttach() {
            _target = null;
            _targetCamera.Follow = null;
            _targetCamera.LookAt = null;
            _targetCamera.gameObject.SetActive(false);
            _targetCamera.transform.position = Vector3.zero;
            _isAttached = false;
        }
        
        public void Destruct() {
            _gameStateController.OnStateChange -= OnStateChange;
        }
    }
    
    [Serializable]
    public struct MiniTransform {
        public Vector3 position;
        public Vector3 rotation;

        public MiniTransform(Vector3 position, Vector3 rotation) {
            this.position = position;
            this.rotation = rotation;
        }
    }
}
