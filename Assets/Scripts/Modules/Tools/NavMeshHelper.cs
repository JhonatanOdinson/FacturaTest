using UnityEngine;
using UnityEngine.AI;

namespace Modules.Tool {
  public static class NavMeshHelper {
    
    public static bool IsOnNavMesh(Vector3 pos, float radius = 1f, int areaMask = NavMesh.AllAreas) {
      return NavMesh.SamplePosition(pos, out var hit, radius, areaMask);
    }

    public static bool GetPointOnNavMesh(Vector3 startPos, out Vector3 endPos, float radius = 1f, int mask = NavMesh.AllAreas) {
      if (NavMesh.SamplePosition(startPos, out var hit, radius, mask)) {
        endPos = hit.position;
        return true;
      }

      endPos = startPos;
      return false;
    }

    public static bool GetPointOnNavmeshLargeRadius(Vector3 startPos, out Vector3 endPos, float defaultRadius,
      float largeRadius, int mask = NavMesh.AllAreas) {
      if (GetPointOnNavMesh(startPos, out endPos, defaultRadius, mask)) return true;
      if (GetPointOnNavMesh(startPos, out endPos, largeRadius, mask)) return true;
      Debug.LogError($"not enough large radius {largeRadius}");
      return false;
    }
  }
}