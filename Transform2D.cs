using ImGuiNET;
using System.Numerics;
namespace Engine
{
    public class Transform2D : Component
    {
        private GameObject? parent;
        private List<GameObject>? children;
        public Vector2 Scale
        {
            get => _Scale;
            set { _Scale = RecalculateScale(value); }
        }
        Vector2 _Scale;
        private Vector2 RecalculateScale(Vector2 newScale)
        {
            if (parent is not null)
            {
                return newScale + parent.Transform.Scale;
            }
            return newScale;
        }
        public float Rotation
        {
            get => _Rotation;
            set { _Rotation = RecalculateRotation(value); }
        }
        float _Rotation;

        private float RecalculateRotation(float newRotation)
        {
            if (parent is not null)
            {
                return newRotation + parent.Transform.Rotation;
            }
            return newRotation;
        }
        public Vector2 Position
        {
            get => _Position;
            set { _Position = RecalculatePosition(value); }
        }
        Vector2 _Position;

        private Vector2 RecalculatePosition(Vector2 newPosition)
        {
            if (parent is not null)
            {
                return newPosition + parent.Transform.Position;
            }
            return newPosition;
        }

        public Transform2D(Vector2 Position, float Rotation, Vector2 Scale)
        {
            this.Position = Position;
            this.Rotation = Rotation;
            this.Scale = Scale;
        }

        public Transform2D()
        {
            this.Position = Vector2.Zero;
            this.Rotation = 0.0f;
            this.Scale = Vector2.Zero;
        }

        public void SetChildren(List<GameObject>? children)
        {
            this.children = children;
        }

        public void SetParent(GameObject? parent) { this.parent = parent; }

        public override void DrawInspector()
        {
            ImGui.DragFloat2("Position", ref _Position);
            ImGui.DragFloat("Rotation", ref _Rotation);
            ImGui.DragFloat2("Scale", ref _Scale);
        }

        public override void EarlyUpdate() { }
        public override void Update() { }
        public override void LateUpdate() { }
    }
}
