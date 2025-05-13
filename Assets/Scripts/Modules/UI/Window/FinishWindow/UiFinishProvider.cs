using Library.Scripts.Core;
using Modules.UI.Interface;
using Unity.Burst.Intrinsics;
using UnityEngine;

namespace Modules.UI.Window.FinishWindow
{
    public class UiFinishProvider : MonoBehaviour, IWindowProvider
    {
        private UiFinishWindow _window;

        public void Init(WindowBase window)
        {
            _window = (UiFinishWindow)window;
            CommonComponents.RunProgressController.OnLevelComplete += OnCompleteLevelHandler;
            CommonComponents.RunProgressController.OnLevelFailed += OnFailedLevelHandler;
            if (_window.Data.ShowOnStart)
                ShowWindow(true);
            else
                HideWindow(true);
        }

        private void OnFailedLevelHandler()
        {
            _window.SetTitles(false);
        }

        private void OnCompleteLevelHandler()
        {
            _window.SetTitles(true);
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
