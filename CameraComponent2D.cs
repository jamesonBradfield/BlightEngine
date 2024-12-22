// CameraComponent2D.cs
using System.Numerics;
using Raylib_cs;
using ImGuiNET;
namespace Engine
{
    /// <summary>
    /// Component that manages 2D camera functionality using Raylib's Camera2D system.
    /// </summary>
    public class CameraComponent2D : Component
    {
        private Scene scene = Scene.Instance;

        /// <summary>
        /// The underlying Raylib Camera2D instance that handles camera transformation.
        /// </summary>
        public Camera2D camera2D;

        /// <summary>
        /// Initializes a new instance of CameraComponent2D with specified parameters.
        /// </summary>
        /// <param name="Offset">The camera's offset from the target position.</param>
        /// <param name="Target">The point the camera is looking at.</param>
        /// <param name="Rotation">The camera's rotation in degrees.</param>
        /// <param name="Zoom">The camera's zoom level (1.0f is normal).</param>
        public CameraComponent2D(Vector2 Offset, Vector2 Target, float Rotation,
                                 float Zoom)
        {
            camera2D = new Camera2D(Offset, Target, Rotation, Zoom);
        }

        /// <summary>
        /// Draws any camera-specific visualization elements.
        /// Currently not implemented.
        /// </summary>
        /// <param name="camera">The camera to render from.</param>
        public override void Draw(Camera2D camera)
        {
            //we might want to implement something here.
        }

        /// <summary>
        /// Draws the ImGui inspector interface for camera properties.
        /// </summary>
        public override void DrawInspector()
        {
            ImGui.DragFloat2("Offset", ref camera2D.Offset);
            ImGui.DragFloat2("Target", ref camera2D.Target);
            ImGui.DragFloat("Rotation", ref camera2D.Rotation);
            ImGui.DragFloat("Zoom", ref camera2D.Zoom);
        }

        // Base component lifecycle methods
        public override void EarlyUpdate() { }
        public override void Update() { }
        public override void LateUpdate() { }
        public override void Initialize() { }
    }
}
