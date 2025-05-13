using Library.Scripts.Core;
using Modules.UI.Interface;
using UnityEngine;

namespace Modules.UI.Window.FadeTransitionWindow
{
    public class UiFadeTransitionProvider : MonoBehaviour, IWindowProvider
    {
        private UiFadeTransitionWindow _window;

        public void Init(WindowBase window)
        {
            _window = (UiFadeTransitionWindow)window;
            if (_window.Data.ShowOnStart)
                ShowWindow(true);
            else
                HideWindow(true);
        }
        

        public void ShowWindow(object ignoreTweens)
        { 
            bool ignore = ignoreTweens is bool ? (bool)ignoreTweens : false;
            _window.Show();
            if(!ignore)
                _window.ShowBg(true);
        }

        public void HideWindow(object ignoreTweens)
        {
            bool ignore = ignoreTweens is bool ? (bool)ignoreTweens : false;
            _window.Hide();
            if(!ignore)
                _window.ShowBg(false);
        }

        public void Destruct()
        {
            
        }
    }
}
