using Core;
using Library.Scripts.Core;
using Modules.UI.Interface;
using Modules.UI.Window.FinishWindow;
using Modules.UI.Window.IntroTipsWindow;
using UnityEngine;

namespace Modules.UI.Window.FadeTransitionWindow
{
    public class UiFadeTransitionHandler : MonoBehaviour, IWindowHandler
    {
        private UiFadeTransitionWindow _window;

        public void Init(WindowBase window)
        {
            _window = (UiFadeTransitionWindow)window;
        }

        public void Destruct()
        {
            
        }
    }
}
