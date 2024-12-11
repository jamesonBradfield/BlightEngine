using ImGuiNET;
using System.Numerics;
namespace Engine {
public class Transform2D : Component {
  public Vector2 Scale {
    get => _Scale;
    set {
      _Scale = value;
      RecalculateScale();
    }
  }
  Vector2 _Scale;
  private void RecalculateScale() { throw new NotImplementedException(); }
  public float Rotation {
    get => _Rotation;
    set {
      _Rotation = value;
      RecalculateRotation();
    }
  }
  float _Rotation;

  private void RecalculateRotation() { throw new NotImplementedException(); }
  public Vector2 Position {
    get => _Position;
    set {
      _Position = value;
      RecalculatePosition();
    }
  }
  Vector2 _Position;

  private void RecalculatePosition() { throw new NotImplementedException(); }

  public Transform2D() {
    this.Position = Vector2.Zero;
    this.Rotation = 0.0f;
    this.Scale = Vector2.One;
    name = GetType().ToString();
  }

  public Transform2D(Vector2 Position) {
    this.Position = Position;
    this.Rotation = 0.0f;
    this.Scale = Vector2.One;
    name = GetType().ToString();
  }

  public Transform2D(Vector2 Position, float Rotation) {
    this.Position = Position;
    this.Rotation = Rotation;
    this.Scale = Vector2.One;
    name = GetType().ToString();
  }

  public Transform2D(Vector2 Position, Vector2 Scale) {
    this.Position = Position;
    this.Rotation = 0.0f;
    this.Scale = Scale;
    name = GetType().ToString();
  }

  public Transform2D(Vector2 Position, float Rotation, Vector2 Scale) {
    this.Position = Position;
    this.Rotation = Rotation;
    this.Scale = Scale;
    name = GetType().ToString();
  }

  public override void DrawInspector() {
    if (ImGui.Button(name)) {
      ImGui.DragFloat2("Position", ref _Position);
      ImGui.DragFloat("Rotation", ref _Rotation);
      ImGui.DragFloat2("Scale", ref _Position);
    }
  }
  public override void EarlyUpdate() {}
  public override void Update() {}
  public override void LateUpdate() {}
}
}
