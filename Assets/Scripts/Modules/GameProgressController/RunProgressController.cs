using System;
using Core;
using Library.Scripts.Core;
using Modules.Character;
using UnityEngine;

namespace Modules.GameProgressController
{
    public class RunProgressController : MonoBehaviour
    {
        [SerializeField] private float _levelRange;
        [SerializeField] private float _progressRange;
        [SerializeField] private bool _isLevelComplete;
        [SerializeField] private int _coinCount;

        private CharacterDataEx _playerDataEx;
        private float playerSpeed = 0f;
        
        public float ProgressRange => _progressRange;
        public int CoinCount => _coinCount;

        public event Action OnLevelFailed;
        public event Action OnLevelComplete;

        public event Action<float> OnProgresUpdate;
        public event Action<int> OnCoinUpdate;
        
        
        public void Init() {
            _levelRange = GameDirector.GetGameConfig.LevelRange;
            CommonComponents.GameStateController.OnStateChange += GameStateChangeHandler;
            CommonComponents.ActorBaseController.BaseEvents.OnActorDeath.Subscribe(null,OnActorDeadHandler);
        }

        private void OnActorDeadHandler(object obj)
        {
            if (obj is HitData hitData)
            {
                if (hitData.Target is CharacterDataEx characterDataEx && !characterDataEx.IsPlayer)
                    AddCoin(characterDataEx.Data.Reward);
            }
        }

        private void AddCoin(int reward)
        {
            _coinCount += reward;
            OnCoinUpdate?.Invoke(_coinCount);
        }

        private void GameStateChangeHandler(GameStateController.GameStateE stateE) {
            if (stateE == GameStateController.GameStateE.Play)
            {
                GetPlayer();
            }

            if (stateE == GameStateController.GameStateE.None)
            {
                _progressRange = 0;
                _isLevelComplete = false;
            }
                
        }

        private void FixedUpdate() {
            CheckProgress();
        }

        private void CheckProgress() {
            if (CommonComponents.GameStateController.CurrentState == GameStateController.GameStateE.Play && !_isLevelComplete) {
                _progressRange += playerSpeed * Time.deltaTime;
                OnProgresUpdate?.Invoke(_progressRange);
                if (_progressRange >= _levelRange)
                {
                    _isLevelComplete = true;
                    OnLevelComplete?.Invoke();
                }
            }
        }

        private void GetPlayer() {
            _playerDataEx = CommonComponents.ActorBaseController.GetPlayer().Data;
            _playerDataEx.OnDeadEvent += OnPlayerDeadHandler;
            playerSpeed = _playerDataEx.Data.Speed;
        }

        private void OnPlayerDeadHandler(CharacterDataEx characterDataEx) {
            _playerDataEx.OnDeadEvent -= OnPlayerDeadHandler;
            OnLevelFailed?.Invoke();
            ResetValues();
        }

        public void ResetValues() {
            _progressRange = 0;
        }

        public void Free()
        {
            CommonComponents.GameStateController.OnStateChange -= GameStateChangeHandler;
            CommonComponents.ActorBaseController.BaseEvents.OnActorDamaged.Unsubscribe(null,OnActorDeadHandler);
            if(_playerDataEx is not null)
                _playerDataEx.OnDeadEvent -= OnPlayerDeadHandler;
        }
    }
}
