using System;
using System.Collections;
using System.Collections.Generic;
using Modules.Emitters;
using Sirenix.OdinInspector;
using UnityEngine;
[Serializable]
public class EmitterData {
  public EmitterPoolData emitter;
  public bool additional;
  [ShowIf("additional")]
  public Vector3 size;
  [ShowIf("additional")]
  public Transform point;
}
