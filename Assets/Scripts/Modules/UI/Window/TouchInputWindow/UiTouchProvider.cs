using Modules.UI.Interface;
using UnityEngine;

namespace Modules.UI.Window.TouchInputWindow
{
    public class UiTouchProvider : MonoBehaviour, IWindowProvider
    {
        private UiTouchInputWindow _window;

        public void Init(WindowBase window)
        {
            _window = (UiTouchInputWindow)window;
            if (_window.Data.ShowOnStart)
                ShowWindow(null);
            else
                HideWindow(null);
        }

        public void ShowWindow(object obj)
        { 
            _window.Show();
        }

        public void HideWindow(object obj)
        {
            _window.Hide();
        }

        public void Destruct()
        {
            
        }
    }
}
