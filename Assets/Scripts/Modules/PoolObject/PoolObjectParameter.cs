using System;
using Modules.Character;
using Modules.PoolObject;
using UnityEngine;

namespace Modules.Emitters {
  public struct PoolObjectParameter {
    public PoolBase PoolObj;
    public PoolDataBase PoolData;
    public Vector3 Pos;
    public Transform Tr;
    public Quaternion Rotation;
    public CharacterDataEx CharacterDataEx;
    public float LifeTime;
    public Transform ForceFieldTr;
    public Transform LineRendererTarget;
    public int Count;
    public Action OnFinish;

    public PoolObjectParameter(PoolDataBase poolData = null, Vector3 pos = new(), Quaternion rotation = new(),
      PoolBase poolObj = null, CharacterDataEx charDataEx = null, Transform tr = null, float lifeTime = 0,
      Transform forceFieldTr = null, Transform lineRendererTarget = null, int count = 0, Action onFinish = null) {
      PoolObj = poolObj;
      PoolData = poolData;
      Pos = pos;
      Rotation = rotation;
      CharacterDataEx = charDataEx;
      Tr = tr;
      LifeTime = lifeTime;
      ForceFieldTr = forceFieldTr;
      LineRendererTarget = lineRendererTarget;
      Count = count;
      OnFinish = onFinish;
    }
  }
}