using Modules.UI.Interface;
using UnityEngine;

namespace Modules.UI.Window.GameInfoWindow
{
    public class UiGameInfoHandler : MonoBehaviour, IWindowHandler
    {
        private UiGameInfoWindow _window;
        //private GameStateController _gameStateController;

        public void Init(WindowBase window)
        {
            _window = (UiGameInfoWindow)window;
            //_gameStateController = CommonComponents.GameStateController;
        }

        public void Destruct()
        {
            
        }

        public void ConfirmClickHandler()
        {
            /*UiTouchInputWindow touchInputWindow = CommonComponents.UiCanvas.GetWindow<UiTouchInputWindow>();
            _gameStateController.ChangeState(GameStateController.GameStateE.Play);
            touchInputWindow.GetProvider().ShowWindow(null);
            _window.GetProvider().HideWindow(null);*/
        }
    }
}
