using Library.Scripts.Modules.Tools;
using Library.Scripts.ScriptableObjects.GameConfig;
using UnityEngine;

namespace Library.Scripts.Core {
    public static class GameDirector {
        private static EnterPoint _enterPoint;
        private static GameConfig _gameConfig;

        public static bool IsGamePaused = false;

        private static float _curTimeScale = 1;

        static GameDirector() {
            LoadData();
        }

        private static void LoadData() {
            _gameConfig = LocalAssetLoader.LoadAsset<GameConfig>("GameConfig", true).Result;
        }

        public static void StopCoroutine(Coroutine routine) {
            _enterPoint.StopCoroutine(routine);
        }

        public static EnterPoint GetEnterPoint => _enterPoint;

       public static GameConfig GetGameConfig => _gameConfig;

        public static void SetEnterPoint(EnterPoint enterPoint) {
            _enterPoint = enterPoint;
        }

        public static void PauseGame() {
           // CommonComponents.TimeController.SetPause(true);
            IsGamePaused = true;
            //_gameConfig.InputController.DisableGameplay();
            Resources.UnloadUnusedAssets();
        }

        public static void UnPauseGame() {
            //CommonComponents.TimeController.SetPause(false);
            IsGamePaused = false;
           // _gameConfig.InputController.EnableGameplay();
        }
    }
}