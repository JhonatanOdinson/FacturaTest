using Modules.Tool;
using Modules.UI.Window;
using UnityEngine;

namespace Library.Scripts.Modules.Ui
{
    [CreateAssetMenu(fileName = "WindowData", menuName = "ScriptableData/UI/WindowData")]
    public class WindowData : ScriptableObject
    {
        [SerializeField] private bool _showOnStart;
        
        [SerializeField] private AssetReferenceInPrefab<WindowBase> _windowRef;

        public AssetReferenceInPrefab<WindowBase> GetWindowRef => _windowRef;
        public bool ShowOnStart => _showOnStart;
    }
}
