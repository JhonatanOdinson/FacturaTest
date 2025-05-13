using DG.Tweening;
using Modules.UI.Interface;
using Modules.UI.Window.StartWindow;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI.Window.FinishWindow
{
    public class UiFinishWindow : WindowBase
    {
        [SerializeField] private UiFinishProvider _provider;
        [SerializeField] private UiFinishHandler _handler;
        [SerializeField] private Image _bgImage;
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private string _titleWin;
        [SerializeField] private string _titleFail;
        [SerializeField] private string _buttonWinTitle;
        [SerializeField] private string _buttonFailTitle;
        [SerializeField] private UiCustomButton _confirmButton;


        private Sequence _sequence;
        public override void Init()
        {
            _provider.Init(this);
            _handler.Init(this);
            _confirmButton.Init();
            _confirmButton.OnClickEvent += OnRestartClickHandler;
        }

        public void SetTitles(bool winState)
        {
            _confirmButton.SetTitle(winState ? _buttonWinTitle : _buttonFailTitle);
            _title.text = winState ? _titleWin : _titleFail;
        }

        private void OnRestartClickHandler()
        {
            _handler.RestartClickHandler();
        }

        public void ShowBg(bool state)
        {
            _title.transform.localScale = Vector3.zero;
            _confirmButton.Show(false);
            _sequence = DOTween.Sequence();
            _sequence.AppendInterval(2f);
            _sequence.AppendCallback(() => {
                _bgImage.DOFade(state ? 0.5f : 0f, 0.5f);
                _title.transform.DOScale(Vector3.one, 0.5f);
                _confirmButton.Show(true);
            });
            _sequence.Play();
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
            _confirmButton.OnClickEvent -= OnRestartClickHandler;
        }

      
    }
}
