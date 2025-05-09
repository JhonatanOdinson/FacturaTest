using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Modules.UI.Window.UiStartWindow {
  public class UiCustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _title;

    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _highLightColor;
    [SerializeField] private Color _disabledColor;

    private bool _interactable = true;
    public event Action OnClickEvent;

    public void Init() {
      _button.onClick.AddListener(OnClickHandler);
      SetTitleColor(_normalColor);
    }

    private void OnClickHandler() {
      Debug.Log($"OnClicked");
      if (!_interactable) return;
      transform.DOScale(new Vector3(0.9f, 0.9f, 0), 0.1f)
        .OnComplete(()=> {
            OnClickEvent?.Invoke();
            //AudioController.UiClose();
            transform.localScale = Vector3.one;
        }).SetLink(gameObject);
    }

    public void SetInteractable(bool value) {
      SetTitleColor((value)?_normalColor:_disabledColor);
      _interactable = value;
    }

    private void SetTitleColor(Color color) {
      _title.color = color;
    }

    public void OnPointerEnter(PointerEventData eventData) {
      transform.DOScale(new Vector3(1.1f, 1.1f, 0), 0.1f);
      SetTitleColor(_highLightColor);
      //AudioController.UiSelect();
    }

    public void OnPointerExit(PointerEventData eventData) {
      transform.DOScale(Vector3.one, 0.1f);
      SetTitleColor(_normalColor);
    }
    public void Show(bool value) {
      if (value) {
        gameObject.SetActive(true);
      }
    }

    public void Free() {
      DOTween.Kill(transform);
    }
  }
}
