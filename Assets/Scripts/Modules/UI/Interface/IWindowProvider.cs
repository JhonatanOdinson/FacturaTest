
using Modules.UI.Window;

namespace Modules.UI.Interface {
  public interface IWindowProvider {
    public void Init(WindowBase windowBase);

    public void ShowWindow(object obj);
    public void HideWindow(object obj);
    
    public void Destruct();
  }
}


