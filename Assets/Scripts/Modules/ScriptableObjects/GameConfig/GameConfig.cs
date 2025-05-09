using Modules.ScriptableObjects;
using UnityEngine;

namespace Library.Scripts.ScriptableObjects.GameConfig
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableData/Core/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private ScenarioData _gameScenario;

        public ScenarioData GameScenario => _gameScenario;
    }
}
