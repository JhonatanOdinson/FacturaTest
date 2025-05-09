using System.Collections.Generic;
using System.Threading.Tasks;
using Modules.Damage;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Character {
  public class CharacterDataController : MonoBehaviour {
    [ShowInInspector, ReadOnly] private List<CharacterData> _characterDataList = new();

    public List<CharacterData> CharacterDatas => _characterDataList;

    public void Init() {
      
    }
    
    public async Task LoadData() {
     
    }
    

    public static void ReceiveDamage(CharacterDataEx target, DamageData damageData) {
      target.ReceiveDamage(damageData);
    }
  }
}