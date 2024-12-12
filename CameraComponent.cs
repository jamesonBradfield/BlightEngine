using System.Numerics;
using Raylib_cs;
using ImGuiNET;
namespace Engine {

public class CameraComponent : Component {
  private Scene scene = Scene.Instance;
  public Camera2D camera2D;
  public Camera3D camera3D;
  // Parameterized constructor for Camera2D
  public CameraComponent(Vector2 Offset, Vector2 Target, float Rotation,
                         float Zoom) {
    camera2D = new Camera2D(Offset, Target, Rotation, Zoom);
  }
  // Parameterized constructor for Camera3D
  public CameraComponent(Vector3 Position, Vector3 Target, Vector3 Up,
                         float FovY, CameraProjection Projection) {
    camera3D = new Camera3D(Position, Target, Up, FovY, Projection);
  }
  // we have done it this way so we might account for culling in the future.
  public override void Draw(Camera2D? camera) {
    foreach (GameObject gameObject in scene.GameObjects) {
      gameObject.Draw(camera);
    }
  }
  public override void Draw(Camera3D? camera) {}
  public override void DrawInspector() {
    if (ImGui.Button(name)) {
      ImGui.DragFloat2("Offset", ref camera2D.Offset);
      ImGui.DragFloat2("Target", ref camera2D.Target);
      ImGui.DragFloat("Rotation", ref camera2D.Rotation);
      ImGui.DragFloat("Zoom", ref camera2D.Zoom);
    }
  }
  public override void EarlyUpdate() {}
  public override void Update() {}
  public override void LateUpdate() {}
  public override void Initialize() {}
}
}
