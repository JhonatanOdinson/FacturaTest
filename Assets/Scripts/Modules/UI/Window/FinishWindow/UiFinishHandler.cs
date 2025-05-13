using Core;
using Library.Scripts.Core;
using Modules.UI.Interface;
using Modules.UI.Window.FadeTransitionWindow;
using Modules.UI.Window.IntroTipsWindow;
using Modules.UI.Window.StartWindow;
using Modules.UI.Window.TouchInputWindow;
using UnityEngine;

namespace Modules.UI.Window.FinishWindow
{
    public class UiFinishHandler : MonoBehaviour, IWindowHandler
    {
        private UiFinishWindow _window;
        private GameStateController _gameStateController;

        public void Init(WindowBase window)
        {
            _window = (UiFinishWindow)window;
            _gameStateController = CommonComponents.GameStateController;
        }

        public void Destruct()
        {
            
        }

        public void RestartClickHandler()
        {
            UiFadeTransitionWindow fadeTransitionWindow = CommonComponents.UiCanvas.GetWindow<UiFadeTransitionWindow>();
            UiTouchInputWindow touchInputWindow = CommonComponents.UiCanvas.GetWindow<UiTouchInputWindow>();
            UiStartWindow startWindow = CommonComponents.UiCanvas.GetWindow<UiStartWindow>();
            _gameStateController.ChangeState(GameStateController.GameStateE.None);
            _window.GetProvider().HideWindow(null);
            touchInputWindow.GetProvider().HideWindow(null);
            fadeTransitionWindow.GetProvider().ShowWindow(null);
            startWindow.GetProvider().ShowWindow(null);
        }
    }
}
