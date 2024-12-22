using System.Numerics;
using Raylib_cs;
using ImGuiNET;
namespace Engine
{

    public class CameraComponent2D : Component
    {
        private Scene scene = Scene.Instance;
        public Camera2D camera2D;
        // Parameterized constructor for Camera2D
        public CameraComponent2D(Vector2 Offset, Vector2 Target, float Rotation,
                                 float Zoom)
        {
            camera2D = new Camera2D(Offset, Target, Rotation, Zoom);
        }
        public override void Draw(Camera2D camera)
        {
            //we might want to implement something here.
        }
        public override void DrawInspector()
        {
            ImGui.DragFloat2("Offset", ref camera2D.Offset);
            ImGui.DragFloat2("Target", ref camera2D.Target);
            ImGui.DragFloat("Rotation", ref camera2D.Rotation);
            ImGui.DragFloat("Zoom", ref camera2D.Zoom);
        }
        public override void EarlyUpdate() { }
        public override void Update() { }
        public override void LateUpdate() { }
        public override void Initialize() { }
    }
}
