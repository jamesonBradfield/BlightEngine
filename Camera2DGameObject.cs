using System.Numerics;
namespace Engine
{
    /// <summary>
    /// Represents a 2D camera game object that manages camera positioning and behavior in a 2D scene.
    /// </summary>
    public class Camera2DGameObject : GameObject
    {
        /// <summary>
        /// The camera component that handles 2D camera functionality.
        /// </summary>
        public CameraComponent2D cameraComponent;

        /// <summary>
        /// Initializes a new instance of Camera2DGameObject with a parent and transform.
        /// </summary>
        /// <param name="Parent">The parent game object in the scene hierarchy.</param>
        /// <param name="Transform">The initial 2D transform for the camera.</param>
        public Camera2DGameObject(GameObject Parent, Transform2D Transform)
            : base(Parent, Transform)
        {
            this.name = "Camera";
            cameraComponent = new(Vector2.Zero, Vector2.One, 0.0f, 1.0f);
            this.AddComponent<CameraComponent2D>(cameraComponent);
        }

        /// <summary>
        /// Initializes a new instance of Camera2DGameObject with a transform.
        /// </summary>
        /// <param name="Transform">The initial 2D transform for the camera.</param>
        public Camera2DGameObject(Transform2D Transform) : base(Transform)
        {
            this.name = "Camera";
            cameraComponent = new(Vector2.Zero, Vector2.One, 0.0f, 1.0f);
            this.AddComponent<CameraComponent2D>(cameraComponent);
        }

        /// <summary>
        /// Initializes a new instance of Camera2DGameObject with default settings.
        /// </summary>
        public Camera2DGameObject() : base()
        {
            this.name = "Camera";
            cameraComponent = new(Vector2.Zero, Vector2.One, 0.0f, 1.0f);
            this.AddComponent<CameraComponent2D>(cameraComponent);
        }

        // Base class method overrides with inherited documentation
        public override void AddComponent<T>(T component) { base.AddComponent(component); }
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
