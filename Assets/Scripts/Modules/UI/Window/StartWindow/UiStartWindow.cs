using Modules.UI.Interface;
using Modules.UI.Window.UiStartWindow;
using UnityEngine;

namespace Modules.UI.Window.StartWindow
{
    public class UiStartWindow : WindowBase
    {
        [SerializeField] private UiStartProvider _provider;
        [SerializeField] private UiStartHandler _handler;
        [SerializeField] private UiCustomButton _startButton;

        public override void Init()
        {
            _provider.Init(this);
            _handler.Init(this);
            _startButton.Init();
            _startButton.OnClickEvent += OnStartClickHandler;
        }

        private void OnStartClickHandler()
        {
            _handler.StartClickHandler();
        }

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public override IWindowProvider GetProvider()
        {
            return _provider;
        }

        public override void FreeWindow()
        {
         
        }
        
        public override void Destruct()
        {
            _startButton.OnClickEvent -= OnStartClickHandler;
        }
    }
}
