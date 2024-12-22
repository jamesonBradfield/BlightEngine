using System.Numerics;
namespace Engine
{
    public class Camera2DGameObject : GameObject
    {
        public CameraComponent2D cameraComponent;
        public Camera2DGameObject(GameObject Parent, Transform2D Transform)
            : base(Parent, Transform)
        {
            this.name = "Camera";
            cameraComponent = new(Vector2.Zero, Vector2.One, 0.0f, 1.0f);
            this.AddComponent<CameraComponent2D>(cameraComponent);
        }
        public Camera2DGameObject(Transform2D Transform) : base(Transform)
        {
            this.name = "Camera";
            cameraComponent = new(Vector2.Zero, Vector2.One, 0.0f, 1.0f);
            this.AddComponent<CameraComponent2D>(cameraComponent);
        }
        public Camera2DGameObject() : base()
        {
            this.name = "Camera";
            cameraComponent = new(Vector2.Zero, Vector2.One, 0.0f, 1.0f);
            this.AddComponent<CameraComponent2D>(cameraComponent);
        }

        public override void AddComponent<T>(T component)
        {
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
