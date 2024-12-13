using System.Numerics;
using Raylib_cs;
namespace Engine {
public class Camera2DGameObject : GameObject {
  public CameraComponent cameraComponent;
  public Camera2DGameObject(string? name, GameObject? Parent,
                            Transform2D? Transform)
      : base(name, Parent, Transform) {
    this.AddComponent<CameraComponent>(
        new(Vector2.Zero, Vector2.One, 0.0f, 0.0f));
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
