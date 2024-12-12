using System.Numerics;
using Raylib_cs;
namespace Engine {
public class Camera2DGameObject : GameObject {
  public CameraComponent cameraComponent;
  public Camera2DGameObject(string? name, GameObject? Parent,
                            Transform2D? Transform)
      : base(name, Parent, Transform) {
    if (name is not null) {
      this.name = name;
    } else {
      this.name = "GameObject";
    }
    if (Parent is not null) {
      this.Parent = Parent;
    } else {
      this.Parent = null;
    }
    if (Transform is not null) {
      this.Transform = Transform;
    } else {
      this.Transform = new Transform2D(null, null, null);
    }
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
