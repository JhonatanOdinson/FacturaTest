using UnityEngine;

namespace Modules.Actor.Weapon
{
    public class WeaponLaserSight : MonoBehaviour
    {
        public LineRenderer lineRenderer;
        public float maxDistance = 100f;

        void Update()
        {
            Vector3 endPoint = transform.position + transform.forward * maxDistance;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, endPoint);
        }
    }
}
