using Raylib_cs;
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
            Raylib.GetWorldToScreen2D(transform.Position, (Camera2D)camera);
            Raylib.DrawRectangle(0, 0, this.size, this.size, this.color);
        }
    }
}
