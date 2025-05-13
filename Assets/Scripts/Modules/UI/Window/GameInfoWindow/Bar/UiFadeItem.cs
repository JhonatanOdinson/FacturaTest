using System;
using DG.Tweening;
using Library.Scripts.Core;
using Library.Scripts.Modules.Ui;
using Modules.Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI.Window.GameInfoWindow.HealthBar
{
  public class UiFadeItem : PoolableItem
  {
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private RectTransform _container;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _fadeTime;
    [SerializeField] private Color _coinColor;
    [SerializeField] private Color _damageColor;
    
    private Transform _anchorPos;
    private bool _isDamage;

    public event Action<UiFadeItem> OnFreeReady;

    public override void New()
    {
      base.New();
      Show(false);
    }

    public override void Free()
    {
      base.Free();
      gameObject.SetActive(false);
    }

    public void Show(bool value)
    {
      gameObject.SetActive(value);
      Fade();
    }

    public void SetValue(int value)
    {
      if(_isDamage)
        _countText.text = value.ToString();
      else
        _countText.text = $"+{value}";
    }

    public void SetType(bool isDamage)
    {
      _icon.enabled = !isDamage;
      _isDamage = isDamage;
      if (isDamage)
        _countText.color = _damageColor;
      else
        _countText.color = _coinColor;
    }

    public void Fade()
    {
      _container.DOLocalMoveY(transform.localPosition.y + 0.5f, _fadeTime).SetAutoKill();
      _icon.DOFade(0, _fadeTime).OnComplete(() => OnFreeReady?.Invoke(this)).SetAutoKill();
      _countText.DOFade(0, _fadeTime).SetAutoKill();
    }

    public void SetTarget(ActorBase target)
    {
      _anchorPos = target.HealthBarAnchor;
      SetPosition(_anchorPos.position);
      Show(true);
    }

    public void SetPosition(Vector3 pos)
    {
      _rectTransform.position = UiCanvas.WorldToUISpace(
        GameDirector.GetEnterPoint.SceneComponentsRef.CameraDirector.Camera,
        pos);
    }
  }
}