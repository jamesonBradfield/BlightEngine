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
    set { _Position = RecalculatePosition(value); }
  }
  Vector2 _Position;

  private Vector2 RecalculatePosition(Vector2 newPosition) {
    if (_gameObject.Parent is not null) {
      return newPosition + _gameObject.Parent.Transform.Position;
    }
    return newPosition;
  }

  public Transform2D(Vector2? Position, float? Rotation, Vector2? Scale) {
    if (Position is not null) {
      this.Position = (Vector2)Position;
    } else {
      this.Position = Vector2.Zero;
    }
    if (Rotation is not null) {
      this.Rotation = (float)Rotation;
    } else {
      this.Rotation = 0.0f;
    }
    if (Scale is not null) {
      this.Scale = (Vector2)Scale;
    } else {
      this.Scale = Vector2.One;
    }
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
