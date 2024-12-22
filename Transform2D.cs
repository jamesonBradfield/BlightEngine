using ImGuiNET;
using System.Numerics;
namespace Engine
{
    /// <summary>
    /// Represents a 2D transform component that handles positioning, rotation, and scaling
    /// of game objects, including parent-child relationship calculations.
    /// </summary>
    public class Transform2D : Component
    {
        private GameObject? parent;
        private List<GameObject>? children;

        /// <summary>
        /// Gets or sets the scale of the transform, accounting for parent scaling.
        /// </summary>
        public Vector2 Scale
        {
            get => _Scale;
            set { _Scale = RecalculateScale(value); }
        }
        private Vector2 _Scale;

        /// <summary>
        /// Recalculates scale based on parent transform.
        /// </summary>
        private Vector2 RecalculateScale(Vector2 newScale)
        {
            if (parent is not null)
            {
                return newScale + parent.Transform.Scale;
            }
            return newScale;
        }

        /// <summary>
        /// Gets or sets the rotation of the transform in degrees, accounting for parent rotation.
        /// </summary>
        public float Rotation
        {
            get => _Rotation;
            set { _Rotation = RecalculateRotation(value); }
        }
        private float _Rotation;

        /// <summary>
        /// Recalculates rotation based on parent transform.
        /// </summary>
        private float RecalculateRotation(float newRotation)
        {
            if (parent is not null)
            {
                return newRotation + parent.Transform.Rotation;
            }
            return newRotation;
        }

        /// <summary>
        /// Gets or sets the position of the transform, accounting for parent position.
        /// </summary>
        public Vector2 Position
        {
            get => _Position;
            set { _Position = RecalculatePosition(value); }
        }
        private Vector2 _Position;

        /// <summary>
        /// Recalculates position based on parent transform.
        /// </summary>
        private Vector2 RecalculatePosition(Vector2 newPosition)
        {
            if (parent is not null)
            {
                return newPosition + parent.Transform.Position;
            }
            return newPosition;
        }

        /// <summary>
        /// Initializes a new instance of the Transform2D class with specified values.
        /// </summary>
        /// <param name="Position">Initial position.</param>
        /// <param name="Rotation">Initial rotation in degrees.</param>
        /// <param name="Scale">Initial scale.</param>
        public Transform2D(Vector2 Position, float Rotation, Vector2 Scale)
        {
            this.Position = Position;
            this.Rotation = Rotation;
            this.Scale = Scale;
        }

        /// <summary>
        /// Initializes a new instance of the Transform2D class with default values.
        /// </summary>
        public Transform2D()
        {
            this.Position = Vector2.Zero;
            this.Rotation = 0.0f;
            this.Scale = Vector2.Zero;
        }

        /// <summary>
        /// Sets the child objects of this transform.
        /// </summary>
        /// <param name="children">List of child game objects.</param>
        public void SetChildren(List<GameObject>? children)
        {
            this.children = children;
        }

        /// <summary>
        /// Sets the parent of this transform.
        /// </summary>
        /// <param name="parent">Parent game object.</param>
        public void SetParent(GameObject? parent) { this.parent = parent; }

        /// <summary>
        /// Draws the inspector interface for the transform component.
        /// </summary>
        public override void DrawInspector()
        {
            ImGui.DragFloat2("Position", ref _Position);
            ImGui.DragFloat("Rotation", ref _Rotation);
            ImGui.DragFloat2("Scale", ref _Scale);
        }

        // Base component lifecycle methods
        public override void EarlyUpdate() { }
        public override void Update() { }
        public override void LateUpdate() { }
    }
}
