using Raylib_cs;
using System.Numerics;
using ImGuiNET;
namespace Engine
{
    public class RendererComponent2D : Component
    {
        protected Transform2D transform;
        public void SetTransform(Transform2D transform)
        {
            this.transform = transform;
        }
        public override void Draw(Camera2D camera) { }
        public override void DrawInspector() { }
        public override void EarlyUpdate() { }
        public override void Update() { }
        public override void LateUpdate() { }
        public override void Initialize() { }
    }
    public class SquareRendererComponent2D : RendererComponent2D
    {
        int size;
        Color color;
        public SquareRendererComponent2D(int size, Color color)
        {
            this.color = color;
            this.size = size;
        }
        public override void Draw(Camera2D camera)
        {
            base.Draw(camera);
            try
            {
                Console.WriteLine($"Transform Position: {transform.Position}");
                Console.WriteLine($"Camera Offset: {camera.Offset}, Zoom: {camera.Zoom}");
                Vector2 screenPosition = Raylib.GetWorldToScreen2D(transform.Position, (Camera2D)camera);
                Console.WriteLine($"Screen Position: {screenPosition}");
                Raylib.DrawRectangle((int)screenPosition.X, (int)screenPosition.Y, this.size, this.size, this.color);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in SquareRenderer2D: {e.Message}");
            }
        }
        public override void DrawInspector()
        {
            Vector4 imguiColor = new Vector4(this.color.R, this.color.G, this.color.B, this.color.A);
            ImGui.DragInt("size", ref this.size);
            ImGui.ColorPicker4("size", ref imguiColor);
        }
    }
}
