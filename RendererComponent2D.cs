using Raylib_cs;
using System.Numerics;
namespace Engine
{
    public class RendererComponent2D : Component
    {
        protected Transform2D transform;
        public void SetTransform(Transform2D transform)
        {
            this.transform = transform;
        }
        public override void Draw(Camera2D? camera) { }
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
        public override void Draw(Camera2D? camera)
        {
            base.Draw(camera);
            try
            {
                Vector2 screenPosition = Raylib.GetWorldToScreen2D(transform.Position, (Camera2D)camera);
                Raylib.DrawRectangle((int)screenPosition.X, (int)screenPosition.Y, this.size, this.size, this.color);
            }
            catch
            {
                Console.WriteLine("camera on " + this + " is null");
            }
        }
    }
}
