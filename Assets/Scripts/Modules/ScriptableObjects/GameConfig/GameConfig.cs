using Modules.ScriptableObjects;
using UnityEngine;

namespace Library.Scripts.ScriptableObjects.GameConfig
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableData/Core/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private ScenarioData _gameScenario;
        [SerializeField] private float _levelRange;

        public ScenarioData GameScenario => _gameScenario;
        public float LevelRange => _levelRange;
    }
}
