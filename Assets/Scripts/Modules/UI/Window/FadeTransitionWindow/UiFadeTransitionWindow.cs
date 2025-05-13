using DG.Tweening;
using Modules.UI.Interface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI.Window.FadeTransitionWindow
{
    public class UiFadeTransitionWindow : WindowBase
    {
        [SerializeField] private UiFadeTransitionProvider _provider;
        [SerializeField] private UiFadeTransitionHandler _handler;
        [SerializeField] private Image _bgImage;
            
        private Sequence _sequence;
        public override void Init()
        {
            _provider.Init(this);
            _handler.Init(this);
        }

        public void ShowBg(bool state)
        {
            if (state)
            {
                _sequence = DOTween.Sequence();
                _sequence.Append(_bgImage.DOFade(1f, 0.5f));
                _sequence.AppendInterval(2f);
                _sequence.Append(_bgImage.DOFade(0f, 0.5f));
                _sequence.Play().OnComplete(()=>_provider.HideWindow(true));
            }
        }

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public void KillTweens()
        {
            _sequence.Kill(true);
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
            KillTweens();
        }

      
    }
}
