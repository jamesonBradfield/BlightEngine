using Raylib_cs;
using ImGuiNET;
namespace Engine {
public class Component {
  public virtual void Draw(Camera2D? camera) {}
  public virtual void Draw(Camera3D? camera) {}
  public virtual void DrawInspector() {}
  public virtual void EarlyUpdate() {}
  public virtual void Update() {}
  public virtual void LateUpdate() {}
  public virtual void Initialize() {}
}
}
