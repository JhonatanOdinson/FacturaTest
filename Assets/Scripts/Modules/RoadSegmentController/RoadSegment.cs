using System;
using Core.GameEvents;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Modules.RoadSegmentController
{
    public class RoadSegment : PoolableItem
    {
        [SerializeField] private Transform _nextSegmentAnchor;
        [SerializeField] private RoadSegmentActivator _activator;
        [SerializeField] private NavMeshSurface _navMeshSurface;
        public Transform NextSegmentAnchor => _nextSegmentAnchor;

        public GameEvent OnPlaceSegment;
        public GameEvent OnFreeSegment;
        
        public event Action OnSegmentActive;
        public event Action OnSegmentFree; 

        public void Init()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            _activator.OnActivate += OnActivationEvent;
        }

        private void OnActivationEvent()
        {
            OnSegmentActive?.Invoke();
            Unsubscribe();
        }

        private void Unsubscribe()
        {
            _activator.OnActivate -= OnActivationEvent;
        }

        public void SetPosition(Vector3 position)
        {
            gameObject.transform.position = position;
            OnPlaceSegment.Check(null,_navMeshSurface);
        }

        public override void Free()
        {
            OnFreeSegment.Check(null,(int)transform.position.z);
            OnSegmentFree?.Invoke();
            Unsubscribe();
            gameObject.SetActive(false);
            base.Free();
        }
    }
}
