using Core;
using Core.GameEvents;
using Library.Scripts.Core;
using Modules.Actor;
using Modules.Actor.Weapon;
using Modules.Character;
using Modules.GameProgressController;
using Modules.UI.Interface;
using Modules.UI.Window.FinishWindow;
using UnityEngine;

namespace Modules.UI.Window.GameInfoWindow
{
    public class UiGameInfoProvider : MonoBehaviour, IWindowProvider
    {
        private UiGameInfoWindow _window;
        private ActorBaseController _actorBaseController;
        private RunProgressController _runProgressController;

        public GameEvent OnActorDeath;
        public GameEvent OnActorDamaged;
        

        public void Init(WindowBase window) {
            _window = (UiGameInfoWindow)window;
            
            _actorBaseController = CommonComponents.ActorBaseController;
            _runProgressController = CommonComponents.RunProgressController;
            _actorBaseController.OnCreateActor += OnCreateActorHandler;
            _actorBaseController.OnDestroyActor += OnDestroyActorHandler;
            _runProgressController.OnProgresUpdate += OnProgressUpdateHandler;
            _runProgressController.OnLevelFailed += OnLevelEndHandler;
            _runProgressController.OnLevelComplete += OnLevelEndHandler;
            _runProgressController.OnCoinUpdate += OnCoinUpdateHandler;
            OnActorDeath.Subscribe(null, OnActorDeathHandler);
            OnActorDamaged.Subscribe(null,OnActorDamagedHandler);
            
            if (_window.Data.ShowOnStart)
                ShowWindow(true);
            else
                HideWindow(true);
        }

        private void OnCoinUpdateHandler(int value)
        {
            _window.UpdateCoin(value);
        }

        private void OnLevelEndHandler()
        {
            UiFinishWindow introTipsWindow = CommonComponents.UiCanvas.GetWindow<UiFinishWindow>();
            CommonComponents.GameStateController.ChangeState(GameStateController.GameStateE.Idle);
            HideWindow(null);
            introTipsWindow.GetProvider().ShowWindow(null);
        }

        private void OnProgressUpdateHandler(float progressValue)
        {
            _window.UpdateProgressBar(progressValue);
        }

        private void OnActorDamagedHandler(object obj)
        {
            if (obj is HitData hitData)
            {
                if (hitData.DamageData.Damager is WeaponDataEx weaponDataEx)
                {
                    if(hitData.Target is CharacterDataEx characterDataEx)
                        if (weaponDataEx.GetOwner.Data != characterDataEx)
                            _window.CreateFadeItem(characterDataEx.GetActorRef, hitData.DamageData.Damage, true);
                }
            }
        }

        private void OnActorDeathHandler(object obj)
        {
            if (obj is HitData hitData)
            {
                if(hitData.Target is CharacterDataEx characterDataEx && !characterDataEx.IsPlayer)
                    _window.CreateFadeItem(characterDataEx.GetActorRef, hitData.DamageData.Damage, false);
            }
        }

        private void OnCreateActorHandler(ActorBase actorBase) {
            _window.CreateBar(actorBase);
            actorBase.Data.OnDeadEvent += OnDeadHandler;
            actorBase.OnFree += OnFreeHandler;
        }

        private void OnFreeHandler(ActorBase actorBase)
        {
            OnDeadHandler(actorBase.Data);
        }

        private void OnDeadHandler(CharacterDataEx dataEx) {
            _window.FreeBar(dataEx.GetActorRef);
            dataEx.OnDeadEvent -= OnDeadHandler;
        }

        private void OnDestroyActorHandler(ActorBase actorBase) {
            _window.FreeBar(actorBase);
            actorBase.Data.OnDeadEvent -= OnDeadHandler;
        }

        public void ShowWindow(object ignoreTweens) {
            _window.Show();
        }

        public void HideWindow(object ignoreTweens) {
            _window.Hide();
        }

        public void Destruct() {
            _actorBaseController.OnCreateActor -= OnCreateActorHandler;
            _actorBaseController.OnDestroyActor -= OnDestroyActorHandler;
            _runProgressController.OnProgresUpdate -= OnProgressUpdateHandler;
            _runProgressController.OnLevelFailed -= OnLevelEndHandler;
            _runProgressController.OnLevelComplete -= OnLevelEndHandler;
            OnActorDeath.Subscribe(null, OnActorDeathHandler);
            OnActorDamaged.Subscribe(null,OnActorDamagedHandler);
        }
    }
}
