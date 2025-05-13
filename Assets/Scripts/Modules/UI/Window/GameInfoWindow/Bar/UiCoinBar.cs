using TMPro;
using UnityEngine;

namespace Modules.UI.Window.GameInfoWindow.Bar
{
    public class UiCoinBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counter;

        public void Init()
        {
            _counter.text = "0";
        }

        public void UpdateBar(float coin)
        {
            _counter.text = coin.ToString();
        }
    }
}
