using Library.Scripts.Core;
using Library.Scripts.Modules.Ui;
using Modules.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI.Window.GameInfoWindow.HealthBar {
  public class UiHealthBarItem : PoolableItem {
    [SerializeField] private Image _bar;
    [SerializeField] private ActorBase _target;
    [SerializeField] private RectTransform _rectTransform;
    private Transform _anchorPos;

    public ActorBase Target => _target;

    public override void New() {
      base.New();
      Show(false);
    }

    public override void Free() {
      base.Free();
      if (_target != null) 
        _target.Data.OnVitalityChange -= VitalityChangeHandler;
      _target = null;
      gameObject.SetActive(false);
    }

    public void Show(bool value) {
      gameObject.SetActive(value);
    }

    public void SetTarget(ActorBase target) {
      _target = target;
      _anchorPos = _target.HealthBarAnchor;
      if (target.Data.IsPlayer)
        transform.localScale = transform.localScale * 3;
      UpdateVitality(target.Data.GetCurrVitality, target.Data.MaxVitality);
      Show(true);
      _target.Data.OnVitalityChange += VitalityChangeHandler;
    }

    private void UpdateVitality(int curVitality, int maxVitality) {
      _bar.fillAmount = (float) curVitality / maxVitality;
    }

    private void VitalityChangeHandler(int vitality) {
      UpdateVitality(_target.Data.GetCurrVitality, _target.Data.MaxVitality);
    }

    public void UpdatePosition() {
      _rectTransform.position = UiCanvas.WorldToUISpace(
        GameDirector.GetEnterPoint.SceneComponentsRef.CameraDirector.Camera,
        _anchorPos.position);
    }
  }
}