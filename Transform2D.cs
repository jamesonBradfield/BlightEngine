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
  public Vector2 Rotation {
    get => _Rotation;
    set {
      _Rotation = value;
      RecalculateRotation();
    }
  }
  Vector2 _Rotation;

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

  public override void DrawInspector() {}
  public override void EarlyUpdate() {}
  public override void Update() {}
  public override void LateUpdate() {}
}
}
