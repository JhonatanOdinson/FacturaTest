using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI.Window.GameInfoWindow.Bar
{
    public class UiProgressBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _meterCounter;
        [SerializeField] private Image _barImage;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _rectTextTransform;
        
        private float _barSize;

        public void Init()
        {
            
        }

        public void SetBarSize(float size)
        {
            _barSize = size;
        }

        public void UpdateBar(float valueProgress)
        {
            float progress = valueProgress / _barSize;
            _barImage.fillAmount = progress;

            _meterCounter.text = $"{Mathf.RoundToInt(valueProgress)} м";
            
            if (_rectTransform != null)
            {
                float fillSize = _rectTransform.rect.height;
                float textPosY = fillSize * progress;
                _rectTextTransform.anchoredPosition = new Vector2(0, textPosY);
            }
        }
    }
}
