using System;
using UnityEngine;

namespace Modules.RoadSegmentController
{
    public class RoadSegmentActivator : MonoBehaviour
    {
        public event Action OnActivate; 
        private void OnTriggerEnter(Collider other)
        {
            OnActivate?.Invoke();
        }
    }
}
