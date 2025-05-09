using UnityEngine;

namespace Modules.Actor.Weapon
{
    public class WeaponLaserSight : WeaponComponentBase
    {
        public LineRenderer lineRenderer;
        public float maxDistance = 100f;

        public override void SetEnabled(bool state)
        {
            base.SetEnabled(state);
            lineRenderer.enabled = state;
        }

        public override void UpdateExecute()
        {
            Vector3 endPoint = transform.position + transform.forward * maxDistance;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, endPoint);
        }
    }
}
