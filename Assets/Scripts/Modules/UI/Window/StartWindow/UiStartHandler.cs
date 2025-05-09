using Core;
using Library.Scripts.Core;
using Modules.UI.Interface;
using Modules.UI.Window.TouchInputWindow;
using UnityEngine;

namespace Modules.UI.Window.StartWindow
{
    public class UiStartHandler : MonoBehaviour, IWindowHandler
    {
        private UiStartWindow _window;
        private GameStateController _gameStateController;

        public void Init(WindowBase window)
        {
            _window = (UiStartWindow)window;
            _gameStateController = CommonComponents.GameStateController;
        }

        public void Destruct()
        {

        }

        public void StartClickHandler()
        {
            Debug.Log($"StartClickHandler H");
            UiTouchInputWindow inputWindow = CommonComponents.UiCanvas.GetWindow<UiTouchInputWindow>();
            _gameStateController.ChangeState(GameStateController.GameStateE.Play);
            inputWindow.GetProvider().ShowWindow(null);
            _window.GetProvider().HideWindow(null);
        }
    }
}
