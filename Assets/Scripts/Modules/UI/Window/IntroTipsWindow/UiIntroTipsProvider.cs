using HutongGames.PlayMaker.Actions;
using Library.Scripts.Core;
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
            _window.Show();
            if(!ignore)
                _window.ShowBg(true);
            CommonComponents.TouchInputController.OnClickPerformed += OnConfirmClickHandler;
        }

        private void OnConfirmClickHandler()
        {
            _window.OnConfirmClickHandler();
        }

        public void HideWindow(object ignoreTweens)
        {
            bool ignore = ignoreTweens is bool ? (bool)ignoreTweens : false;
            _window.Hide();
            if(!ignore)
             _window.ShowBg(false);
            CommonComponents.TouchInputController.OnClickPerformed -= OnConfirmClickHandler;
        }

        public void Destruct()
        {
            
        }
    }
}
