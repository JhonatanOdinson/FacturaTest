using Library.Scripts.Modules.Ui;
using Modules.Objects.Interface;
using UnityEngine;

namespace Modules.UI.Interface {
  public interface IWindow : IShowable, IDestructable {
    public WindowData Data { get; }
    public GameObject gameObject { get; }
    public bool IsActive { get; }
    public void Init();
    public void FreeWindow();
  }
}
