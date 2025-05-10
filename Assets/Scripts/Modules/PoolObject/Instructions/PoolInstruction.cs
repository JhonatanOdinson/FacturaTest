using System.Threading.Tasks;
using UnityEngine;

namespace Modules.Emitters {
  public class PoolInstruction : ScriptableObject {
    public virtual void Invoke(PoolObjectParameter param) { }

    public virtual async Task IsFinished(PoolObjectParameter param, bool instantly) {
    }
  }
}