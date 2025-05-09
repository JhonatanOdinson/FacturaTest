namespace Modules.Tool.UniversalPool {
  public interface IPoolable {
    void Initialize(IPool pool);
    void New();

    void Free();

  }
}