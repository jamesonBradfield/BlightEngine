using Raylib_cs;
using ImGuiNET;
using System.Numerics;
using rlImGui_cs;

namespace Engine
{
    public class SceneView
    {
        private RenderTexture2D renderTexture;
        private Scene scene;
        private Vector2 lastSize = Vector2.Zero;

        public SceneView(Scene scene)
        {
            this.scene = scene;
            // Initialize with a default size
            renderTexture = Raylib.LoadRenderTexture(800, 600);
        }

        public void Draw()
        {
            if (!ImGui.Begin("Scene View"))
            {
                ImGui.End();
                return;
            }

            // Get available size for the scene view
            Vector2 viewSize = ImGui.GetContentRegionAvail();

            // Recreate render texture if window is resized
            if (viewSize.X != lastSize.X || viewSize.Y != lastSize.Y)
            {
                if (viewSize.X > 0 && viewSize.Y > 0)
                {
                    // Unload old texture
                    Raylib.UnloadRenderTexture(renderTexture);

                    // Create new texture with new size
                    renderTexture = Raylib.LoadRenderTexture((int)viewSize.X, (int)viewSize.Y);
                    lastSize = viewSize;
                }
            }

            // Begin drawing to render texture
            Raylib.BeginTextureMode(renderTexture);
            {
                Raylib.ClearBackground(Color.DarkGray);

                Raylib.BeginMode2D(scene.activeCamera);

                // Draw scene contents
                scene.Draw();

                Raylib.EndMode2D();
            }
            Raylib.EndTextureMode();

            // Draw the render texture in ImGui
            rlImGui.ImageRenderTexture(renderTexture);

            ImGui.End();
        }

        public void Dispose()
        {
            Raylib.UnloadRenderTexture(renderTexture);
        }

        // Get mouse position in scene coordinates
        public Vector2 GetSceneMousePosition()
        {
            if (!ImGui.IsWindowHovered())
                return Vector2.Zero;

            Vector2 windowPos = ImGui.GetWindowPos();
            Vector2 mousePos = ImGui.GetMousePos();
            Vector2 contentRegionOffset = ImGui.GetWindowContentRegionMin();

            // Calculate relative position within scene view
            Vector2 relativePos = mousePos - windowPos - contentRegionOffset;

            // Convert to world space
            return Raylib.GetScreenToWorld2D(
                relativePos,
                (Camera2D)scene.activeCamera
            );
        }
    }
}
