using System.Numerics;
using Raylib_cs;
using ImGuiNET;
namespace Engine {

public class CameraComponent : Component {
  Camera2D camera2D;
  Camera3D camera3D;
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
  public override void Draw(Camera2D camera) {}
  public override void Draw(Camera3D camera) {}
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

public class Camera : GameObject {
  public CameraComponent cameraComponent;
  public Camera(string name, GameObject Parent, Transform2D Transform)
      : base(name, Parent, Transform) {
    this.name = name;
    this.Parent = Parent;
    this.Transform = Transform;
    cameraComponent = new(Vector2.Zero, Vector2.One, 0.0f, 0.0f);
  }

  public Camera(string name) : base(name) {
    this.name = name;
    cameraComponent = new(Vector2.Zero, Vector2.One, 0.0f, 0.0f);
  }

  public Camera() {
    this.name = "Camera";
    this.Parent = null;
    this.Transform = new Transform2D();
    cameraComponent = new(Vector2.Zero, Vector2.One, 0.0f, 0.0f);
  }

  public override void AddComponent<T>(T component) {
    base.AddComponent(component);
  }

  public override void DrawInspector() { base.DrawInspector(); }

  public override void EarlyUpdate() { base.EarlyUpdate(); }

  public override bool Equals(object? obj) { return base.Equals(obj); }

  public override int GetHashCode() { return base.GetHashCode(); }

  public override void Initialize() { base.Initialize(); }

  public override void LateUpdate() { base.LateUpdate(); }

  public override string? ToString() { return base.ToString(); }

  public override void Update() { base.Update(); }
}
}
