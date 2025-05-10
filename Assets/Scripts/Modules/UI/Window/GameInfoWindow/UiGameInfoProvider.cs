using Library.Scripts.Core;
using Modules.Actor;
using Modules.Character;
using Modules.UI.Interface;
using UnityEngine;

namespace Modules.UI.Window.GameInfoWindow
{
    public class UiGameInfoProvider : MonoBehaviour, IWindowProvider
    {
        private UiGameInfoWindow _window;
        private ActorBaseController _actorBaseController;
        

        public void Init(WindowBase window) {
            _window = (UiGameInfoWindow)window;
            
            _actorBaseController = CommonComponents.ActorBaseController;
            _actorBaseController.OnCreateActor += OnCreateActorHandler;
            _actorBaseController.OnDestroyActor += OnDestroyActorHandler;
            
            if (_window.Data.ShowOnStart)
                ShowWindow(true);
            else
                HideWindow(true);
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
            
        }
    }
}
