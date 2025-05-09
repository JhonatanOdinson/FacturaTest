using Library.Scripts.Modules.Ui;
using Modules.UI.Interface;
using UnityEngine;

namespace Modules.UI.Window
{
   public abstract class WindowBase : MonoBehaviour,IWindow
   {
      [SerializeField] private WindowData _data;

      public WindowData Data => _data;

      public bool IsActive => gameObject.activeSelf;
      public abstract void Init();

      public abstract void FreeWindow();

      public abstract void Show();

      public abstract void Hide();
      
      public abstract IWindowProvider GetProvider();

      public abstract void Destruct();
   }
}
