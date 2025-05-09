using Core;
using Library.Scripts.Core;
using Modules.UI.Interface;
using Modules.UI.Window.TouchInputWindow;
using UnityEngine;

namespace Modules.UI.Window.IntroTipsWindow
{
    public class UiIntroTipsHandler : MonoBehaviour, IWindowHandler
    {
        private UiIntroTipsWindow _window;
        private GameStateController _gameStateController;

        public void Init(WindowBase window)
        {
            _window = (UiIntroTipsWindow)window;
            _gameStateController = CommonComponents.GameStateController;
        }

        public void Destruct()
        {
            
        }

        public void ConfirmClickHandler()
        {
            UiTouchInputWindow touchInputWindow = CommonComponents.UiCanvas.GetWindow<UiTouchInputWindow>();
            _gameStateController.ChangeState(GameStateController.GameStateE.Play);
            touchInputWindow.GetProvider().ShowWindow(null);
            _window.GetProvider().HideWindow(null);
        }
    }
}
