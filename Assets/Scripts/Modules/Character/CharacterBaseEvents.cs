using Core.GameEvents;
using SOE.GameEvents;
using UnityEngine;

namespace Modules.Character {
  [CreateAssetMenu(fileName = "CharacterBaseEvents", menuName = "ScriptableData/Character/CharacterBaseEvents", order = 0)]
  public class CharacterBaseEvents : ScriptableObject {
    [SerializeField] private GameEvent _onActorDeath;
    [SerializeField] private GameEvent _onActorDamage;
    [SerializeField] private GameEvent _onActorSpawn;
    [SerializeField] private GameEvent _onActorRespawn;

    public GameEvent OnActorDeath => _onActorDeath;
    public GameEvent OnActorDamaged => _onActorDamage;
    public GameEvent OnActorSpawn => _onActorSpawn;
    public GameEvent OnActorRespawn => _onActorRespawn;
  }
}