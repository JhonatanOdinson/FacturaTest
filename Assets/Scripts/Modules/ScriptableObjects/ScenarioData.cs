using System.Collections.Generic;
using Modules.Character;
using UnityEngine;

namespace Modules.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ScenarioData", menuName = "ScriptableData/Scenario/ScenarioData", order = 0)]
    public class ScenarioData : ScriptableObject
    {
        [SerializeField] private Vector2Int _enemyPerSegment;
        [SerializeField] private List<CharacterData> _enemyList;

        [SerializeField] private CharacterData _player;

        public Vector2Int EnemyPerSegment => _enemyPerSegment;
        public List<CharacterData> EnemyList => _enemyList;
        public CharacterData Player => _player;
    }
}
