using Raylib_cs;
using System.Numerics;
using ImGuiNET;
namespace Engine
{
    /// <summary>
    /// Base class for 2D rendering components. Provides common functionality
    /// for rendering 2D graphics in the game engine.
    /// </summary>
    public class RendererComponent2D : Component
    {
        protected Transform2D transform;

        /// <summary>
        /// Sets the transform component used for positioning and orientation.
        /// </summary>
        /// <param name="transform">The transform to use for rendering.</param>
        public void SetTransform(Transform2D transform)
        {
            this.transform = transform;
        }

        // Base implementation of component methods
        public override void Draw(Camera2D camera) { }
        public override void DrawInspector() { }
        public override void EarlyUpdate() { }
        public override void Update() { }
        public override void LateUpdate() { }
        public override void Initialize() { }
    }

    /// <summary>
    /// Renders a square shape in 2D space with customizable size and color.
    /// </summary>
    public class SquareRendererComponent2D : RendererComponent2D
    {
        private int size;
        private Color color;

        /// <summary>
        /// Initializes a new square renderer with specified size and color.
        /// </summary>
        /// <param name="size">The size of the square in pixels.</param>
        /// <param name="color">The color of the square.</param>
        public SquareRendererComponent2D(int size, Color color)
        {
            this.color = color;
            this.size = size;
        }

        /// <summary>
        /// Draws the square using the current transform and camera settings.
        /// </summary>
        /// <param name="camera">The camera to use for rendering.</param>
        public override void Draw(Camera2D camera)
        {
            base.Draw(camera);
            try
            {
                Vector2 screenPosition = Raylib.GetWorldToScreen2D(transform.Position, camera);
                Raylib.DrawRectangle((int)screenPosition.X, (int)screenPosition.Y, 
                                   this.size, this.size, this.color);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in SquareRenderer2D: {e.Message}");
            }
        }

        /// <summary>
        /// Draws the inspector interface for modifying square properties.
        /// </summary>
        public override void DrawInspector()
        {
            Vector4 imguiColor = new Vector4(this.color.R, this.color.G, 
                                           this.color.B, this.color.A);
            ImGui.DragInt("size", ref this.size);
            ImGui.ColorPicker4("size", ref imguiColor);
        }
    }
}
