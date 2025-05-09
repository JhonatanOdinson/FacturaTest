using HutongGames.PlayMaker.Actions;
using Modules.UI.Interface;
using Modules.UI.Window.StartWindow;
using UnityEngine;

namespace Modules.UI.Window.IntroTipsWindow
{
    public class UiIntroTipsProvider : MonoBehaviour, IWindowProvider
    {
        private UiIntroTipsWindow _window;

        public void Init(WindowBase window)
        {
            _window = (UiIntroTipsWindow)window;
            if (_window.Data.ShowOnStart)
                ShowWindow(true);
            else
                HideWindow(true);
        }

        public void ShowWindow(object ignoreTweens)
        {
            bool ignore = ignoreTweens is bool ? (bool)ignoreTweens : false;
            Debug.Log($"Ignore: {ignore}");
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
