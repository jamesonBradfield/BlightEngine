using System.Numerics;
using Raylib_cs;
using ImGuiNET;
namespace Engine {

public class CameraComponent2D : Component {
  private Scene scene = Scene.Instance;
  public Camera2D camera2D;
  // Parameterized constructor for Camera2D
  public CameraComponent2D(Vector2 Offset, Vector2 Target, float Rotation,
                           float Zoom) {
    camera2D = new Camera2D(Offset, Target, Rotation, Zoom);
  }
  // we have done it this way so we might account for culling in the future.
  public override void Draw(Camera2D? camera) {
    foreach (GameObject gameObject in scene.GameObjects) {
      if (gameObject is not Camera2DGameObject) {
        gameObject.Draw(camera);
      }
    }
  }
  public override void DrawInspector() {
    ImGui.DragFloat2("Offset", ref camera2D.Offset);
    ImGui.DragFloat2("Target", ref camera2D.Target);
    ImGui.DragFloat("Rotation", ref camera2D.Rotation);
    ImGui.DragFloat("Zoom", ref camera2D.Zoom);
  }
  public override void EarlyUpdate() {}
  public override void Update() {}
  public override void LateUpdate() {}
  public override void Initialize() {}
}
}
