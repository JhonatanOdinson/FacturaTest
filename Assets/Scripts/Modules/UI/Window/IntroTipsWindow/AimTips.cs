using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI.Window.IntroTipsWindow
{
    public class AimTips : MonoBehaviour
    {
        [SerializeField] private Image _aimSprite;
        [SerializeField] private float _moveX;
        [SerializeField] private float _moveTime;
        
        public void PlayTips() {
            _aimSprite.transform.localPosition = new Vector3(-_moveX,0,0);
            _aimSprite.transform.DOLocalMoveX(_moveX, _moveTime).SetId("AimMove").SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
        }

        public void StopTips() {
            DOTween.Kill("AimMove");
        }
    }
}
