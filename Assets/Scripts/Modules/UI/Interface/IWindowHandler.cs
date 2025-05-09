using Modules.UI.Window;

namespace Modules.UI.Interface {
  public interface IWindowHandler {
    public void Init(WindowBase window);
    public void Destruct();

  }
}
