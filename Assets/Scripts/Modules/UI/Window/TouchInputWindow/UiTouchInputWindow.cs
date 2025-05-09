using System.Collections.Generic;
using System.Linq;
using Modules.Tool.UniversalPool;
using Modules.UI.Interface;
using UnityEngine;

namespace Modules.UI.Window.TouchInputWindow
{
    public class UiTouchInputWindow : WindowBase
    {
        [SerializeField] private UiTouchProvider _provider;
        [SerializeField] private UiTouchInputHandler _handler;

        public override void Init()
        {
            _provider.Init(this);
            _handler.Init(this);
        }

        public override void FreeWindow()
        {
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

        public override void Destruct()
        {
        }
    }
}
