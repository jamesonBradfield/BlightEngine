using Raylib_cs;
namespace Engine {
public class Component {
  protected GameObject _gameObject { set; private get; }
  public GameObject gameObject {
    set { _gameObject = value; }
    get { return _gameObject; }
  }
  public virtual void Draw(Camera2D camera) {}
  public virtual void Draw(Camera3D camera) {}
  public virtual void DrawInspector() {}
  public virtual void EarlyUpdate() {}
  public virtual void Update() {}
  public virtual void LateUpdate() {}
  public virtual void Initialize() {}
}
}
