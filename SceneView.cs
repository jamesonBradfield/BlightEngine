using Raylib_cs;
using ImGuiNET;
using System.Numerics;
using rlImGui_cs;

namespace Engine
{
    /// <summary>
    /// Provides a visual editor view of the game scene using ImGui and Raylib rendering.
    /// Handles scene rendering, window resizing, and mouse input in the editor.
    /// </summary>
    public class SceneView
    {
        private RenderTexture2D renderTexture;
        private Scene scene;
        private Vector2 lastSize = Vector2.Zero;

        /// <summary>
        /// Initializes a new instance of the SceneView class.
        /// </summary>
        /// <param name="scene">The scene to be viewed and edited.</param>
        public SceneView(Scene scene)
        {
            this.scene = scene;
            renderTexture = Raylib.LoadRenderTexture(800, 600);
        }

        /// <summary>
        /// Draws the scene view window and handles rendering of the scene.
        /// Manages render texture resizing when the window is resized.
        /// </summary>
        public void Draw()
        {
            if (!ImGui.Begin("Scene View"))
            {
                ImGui.End();
                return;
            }

            Vector2 viewSize = ImGui.GetContentRegionAvail();

            if (viewSize.X != lastSize.X || viewSize.Y != lastSize.Y)
            {
                if (viewSize.X > 0 && viewSize.Y > 0)
                {
                    Raylib.UnloadRenderTexture(renderTexture);
                    renderTexture = Raylib.LoadRenderTexture((int)viewSize.X, (int)viewSize.Y);
                    lastSize = viewSize;
                }
            }

            Raylib.BeginTextureMode(renderTexture);
            {
                Raylib.ClearBackground(Color.DarkGray);
                Raylib.BeginMode2D(scene.activeCamera);
                scene.Draw();
                Raylib.EndMode2D();
            }
            Raylib.EndTextureMode();

            rlImGui.ImageRenderTexture(renderTexture);
            ImGui.End();
        }

        /// <summary>
        /// Releases resources used by the scene view.
        /// </summary>
        public void Dispose()
        {
            Raylib.UnloadRenderTexture(renderTexture);
        }

        /// <summary>
        /// Converts mouse position to scene coordinates.
        /// </summary>
        /// <returns>The mouse position in scene space coordinates.</returns>
        public Vector2 GetSceneMousePosition()
        {
            if (!ImGui.IsWindowHovered())
                return Vector2.Zero;

            Vector2 windowPos = ImGui.GetWindowPos();
            Vector2 mousePos = ImGui.GetMousePos();
            Vector2 contentRegionOffset = ImGui.GetWindowContentRegionMin();

            Vector2 relativePos = mousePos - windowPos - contentRegionOffset;

            return Raylib.GetScreenToWorld2D(
                relativePos,
                scene.activeCamera
            );
        }
    }
}
