using Modules.UI.Interface;
using UnityEngine;

namespace Modules.UI.Window.TouchInputWindow
{
    public class UiTouchInputHandler : MonoBehaviour,IWindowHandler
    {
        private UiTouchInputWindow _window;
        public void Init(WindowBase window)
        {
            _window = (UiTouchInputWindow)window;
        }

        public void Destruct()
        {
          
        }
    }
}
