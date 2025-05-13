using System.Linq;
using DG.Tweening;
using Library.Scripts.Core;
using Modules.Character;
using Modules.Tool.UniversalPool;
using Modules.UI.Interface;
using Modules.UI.Window.GameInfoWindow.Bar;
using Modules.UI.Window.GameInfoWindow.HealthBar;
using UnityEngine;

namespace Modules.UI.Window.GameInfoWindow
{
    public class UiGameInfoWindow : WindowBase
    {
        [SerializeField] private UiGameInfoProvider _provider;
        [SerializeField] private UiGameInfoHandler _handler;
        [SerializeField] private UniversalPool<UiHealthBarItem> _healthBarPool;
        [SerializeField] private UniversalPool<UiFadeItem> _fadeItemPool;
        [SerializeField] private UiProgressBar _progressBar;
        [SerializeField] private UiCoinBar _coinBar;
        public override void Init()
        {
            _provider.Init(this);
            _handler.Init(this);
            _healthBarPool.Initialize();
            _fadeItemPool.Initialize();
            _progressBar.SetBarSize(GameDirector.GetGameConfig.LevelRange);
            _coinBar.Init();
        }

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public void ShowBg(bool state)
        {
        }

        public void UpdateProgressBar(float valueProgress)
        {
            _progressBar.UpdateBar(valueProgress);
        }

        public void UpdateCoin(int value)
        {
            _coinBar.UpdateBar(value);
        }
        

        public void CreateBar(ActorBase actorBase) {
            UiHealthBarItem healthBar = _healthBarPool.Take();
            healthBar.SetTarget(actorBase);
        }

        public void CreateFadeItem(ActorBase actorBase, int value, bool isDamage) {
            UiFadeItem fadeItem = _fadeItemPool.Take();
            fadeItem.SetTarget(actorBase);
            fadeItem.SetType(isDamage);
            fadeItem.SetValue(value);
            fadeItem.OnFreeReady += FreeFadeItem;
        }

        public void FreeBar(ActorBase actorBase) {
            UiHealthBarItem healthBar = _healthBarPool.GetBusy().ToList().Find(e => e.Target == actorBase);
            if(healthBar == null) return;
            _healthBarPool.Return(healthBar);
        }

        public void FreeFadeItem(UiFadeItem fadeItem)
        {
            if(fadeItem == null) return;
            _healthBarPool.Return(fadeItem);
        }

        public void LateUpdate() {
            foreach (var uiHealthBarItem in _healthBarPool.GetBusy()) {
                uiHealthBarItem.UpdatePosition();
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
            _provider.Destruct();
            _handler.Destruct();
            KillTweens();
        }
    }
}
