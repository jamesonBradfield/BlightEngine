using ImGuiNET;
using System.Numerics;
using Raylib_cs;

namespace Engine
{
    /// <summary>
    /// Manages the main editor user interface using ImGui, providing a dockable workspace
    /// for game development tools like the hierarchy, inspector, and scene view.
    /// </summary>
    public class EditorUI
    {
        private bool dockspaceOpen = true;
        private ImGuiDockNodeFlags dockspaceFlags = ImGuiDockNodeFlags.None;
        private Editor2D editor = Editor2D.Instance;
        private SceneView sceneView = new SceneView(Scene.Instance);
        private ImGuiProfiler profiler = new();

        /// <summary>
        /// Draws the complete editor interface, including the dockspace and all editor windows.
        /// Sets up the main dockspace window and manages the layout of all child windows.
        /// </summary>
        public void Draw()
        {
            // Set up window flags for the main dockspace window
            ImGuiWindowFlags windowFlags = ImGuiWindowFlags.MenuBar |
                                         ImGuiWindowFlags.NoDocking |
                                         ImGuiWindowFlags.NoTitleBar |
                                         ImGuiWindowFlags.NoCollapse |
                                         ImGuiWindowFlags.NoResize |
                                         ImGuiWindowFlags.NoMove |
                                         ImGuiWindowFlags.NoBringToFrontOnFocus |
                                         ImGuiWindowFlags.NoNavFocus;

            // Get the main viewport
            ImGuiViewportPtr mainViewport = ImGui.GetMainViewport();

            // Set the window to fill the entire viewport
            ImGui.SetNextWindowPos(mainViewport.Pos);
            ImGui.SetNextWindowSize(mainViewport.Size);
            ImGui.SetNextWindowViewport(mainViewport.ID);

            // Remove window padding
            ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 0.0f);
            ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0.0f);
            ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, Vector2.Zero);

            // Create the main dockspace window
            ImGui.Begin("DockSpace", ref dockspaceOpen, windowFlags);
            ImGui.PopStyleVar(3);

            // Submit the DockSpace
            uint dockspaceId = ImGui.GetID("MyDockSpace");
            ImGui.DockSpace(dockspaceId, Vector2.Zero, dockspaceFlags);

            // Draw menu bar (optional)
            if (ImGui.BeginMenuBar())
            {
                if (ImGui.BeginMenu("Options"))
                {
                    ImGui.MenuItem("Close", null, false, false);
                    ImGui.EndMenu();
                }
                ImGui.EndMenuBar();
            }

            // Example windows that can be docked
			profiler.BeginProfile("FrameTime");
			profiler.BeginProfile("Hierarchy");
            ImGui.Begin("Hierarchy");
            // Your hierarchy content here
            editor.DrawInspector();
            ImGui.End();
			profiler.EndProfile("Hierarchy");
			profiler.BeginProfile("Inspector");
            ImGui.Begin("Inspector");
            // Your inspector content here
            editor.DrawSelectedGameObjectsComponents();
            ImGui.End();
			profiler.EndProfile("Inspector");
			profiler.BeginProfile("Scene View");
            ImGui.Begin("Scene View");
            // Your scene view content here
            sceneView.Draw();
            ImGui.End();
			profiler.EndProfile("Scene View");
			profiler.Update(Raylib.GetFrameTime());
			profiler.EndProfile("FrameTime");
            ImGui.End(); // End DockSpace
        }
   		/// <summary>
        /// Releases resources used by the editor UI, particularly the profiler.
        /// </summary>
        public void Dispose()
        {
            profiler.Dispose();
        }
    }
}
