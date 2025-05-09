using Modules.UI.Interface;
using UnityEngine;

namespace Modules.UI.Window.StartWindow
{
    public class UiStartProvider : MonoBehaviour, IWindowProvider
    {
        private UiStartWindow _window;

        public void Init(WindowBase window)
        {
            _window = (UiStartWindow)window;
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
