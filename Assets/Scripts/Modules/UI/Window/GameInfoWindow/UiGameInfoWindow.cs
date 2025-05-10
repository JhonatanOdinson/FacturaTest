using System.Linq;
using DG.Tweening;
using Modules.Character;
using Modules.Tool.UniversalPool;
using Modules.UI.Interface;
using Modules.UI.Window.UiHealthBar;
using UnityEngine;

namespace Modules.UI.Window.GameInfoWindow
{
    public class UiGameInfoWindow : WindowBase
    {
        [SerializeField] private UiGameInfoProvider _provider;
        [SerializeField] private UiGameInfoHandler _handler;
        [SerializeField] private UniversalPool<UiHealthBarItem> _healthBarPool;
        [SerializeField] private RectTransform _healthContainer;
        public override void Init()
        {
            _provider.Init(this);
            _handler.Init(this);
            _healthBarPool.Initialize();
        }

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public void ShowBg(bool state)
        {
        }

        public void CreateBar(ActorBase actorBase) {
            UiHealthBarItem healthBar = _healthBarPool.Take();
            healthBar.SetTarget(actorBase);
        }

        public void FreeBar(ActorBase actorBase) {
            UiHealthBarItem healthBar = _healthBarPool.GetBusy().ToList().Find(e => e.Target == actorBase);
            if(healthBar == null) return;
            _healthBarPool.Return(healthBar);
        }

        public void LateUpdate() {
            foreach (var uiHealthBarItem in _healthBarPool.GetBusy()) {
                uiHealthBarItem.UpdatePosition(_healthContainer);
            }
        }
        
        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public void KillTweens()
        {
            DOTween.Kill("ShowBg",true);
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
