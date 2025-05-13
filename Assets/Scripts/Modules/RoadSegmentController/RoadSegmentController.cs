using System.Collections.Generic;
using System.Linq;
using Core;
using Core.GameEvents;
using Modules.Tool.UniversalPool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.RoadSegmentController
{
    public class RoadSegmentController : MonoBehaviour
    {
        [SerializeField] private int _startSegmentCount = 2;
        [SerializeField] private UniversalPool<RoadSegment> _roadSegmentPool;
        [SerializeField] private GameEvent _onChangeGameState;
        
        public void Init()
        {
            _roadSegmentPool.Initialize();
            _onChangeGameState?.Subscribe(this,OnChangeGameStateHandler);
            PlaceStartSegment();
        }
        
        private void OnChangeGameStateHandler(object gameState)
        {
            if(gameState is GameStateController.GameStateE gameStateE)
                if(gameStateE == GameStateController.GameStateE.None)
                    Restart();
        }

        public void Restart()
        {
            List<RoadSegment> roadList = _roadSegmentPool.GetBusy().ToList();
            for(int i = 0;i<roadList.Count;i++)
            {
                ReturnSegment(roadList[i]);
            }

            PlaceStartSegment();
        }

        private void PlaceStartSegment()
        {
            for (int i = 0; i < _startSegmentCount; i++)
            {
                PlaceSegment(i == 0);
            }
        }

        private void OnSegmentActivateHandler()
        {
            PlaceSegment();
        }

        [Button]
        private void PlaceSegment(bool isStartSegment = false)
        {
            var segmentList = _roadSegmentPool.GetBusy().ToList();
            var segment = TakeSegment();
            segment.SetStartSegment(isStartSegment);
            if (segmentList.Any())
            {
                segment.SetPosition(segmentList.Last().NextSegmentAnchor.position);
                if(segmentList.Count > 2) ReturnSegment(segmentList.First());
            }
            else
            {
                segment.SetPosition(Vector3.zero);     
            }
        }

        private RoadSegment TakeSegment()
        {
            var segment = _roadSegmentPool.Take();
            segment.OnSegmentActive += OnSegmentActivateHandler;
            segment.Init();
            segment.gameObject.SetActive(true);
            return segment;
        }

        private void ReturnSegment(RoadSegment segment)
        {
            segment.OnSegmentActive -= OnSegmentActivateHandler;
            segment.gameObject.SetActive(false);
            _roadSegmentPool.Return(segment);
        }

        public void Free()
        {
            foreach (var segment in _roadSegmentPool.GetBusy())
            {
                segment.OnSegmentActive -= OnSegmentActivateHandler;
            }
            _roadSegmentPool.Clear();
        }
    }
}
