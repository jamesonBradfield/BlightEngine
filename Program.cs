using Raylib_cs;
using ImGuiNET;
using rlImGui_cs;
using System.Numerics;
using Engine;

Raylib.InitWindow(1000, 1000,
                  "Camera2D component system");
rlImGui.Setup(true);
Scene scene = Scene.Instance;
Editor2D editor = Editor2D.Instance;
Camera2DGameObject cam = new Camera2DGameObject(new(Vector2.Zero, 0.0f, Vector2.Zero));

// Create profiler instance
var profiler = new ImGuiProfiler();

scene.AddGameObject(cam);
scene.activeCamera = cam.cameraComponent.camera2D;
GameObject square = new GameObject(cam, new(Vector2.Zero, 0.0f, Vector2.Zero));
square.AddComponent<SquareRendererComponent2D>(new SquareRendererComponent2D(400, Color.Black));
scene.AddGameObject(square);

while (!Raylib.WindowShouldClose())
{
    profiler.BeginProfile("Frame Time");

    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.Gray);

    rlImGui.Begin();

    // Profile your editor window
    profiler.BeginProfile("Editor Window");
    ImGui.Begin("SelectedGameObject");
    editor.DrawSelectedGameObjectsComponents();
    ImGui.End();
    profiler.EndProfile("Editor Window");

    // Profile hierarchy window
    profiler.BeginProfile("Hierarchy Window");
    ImGui.Begin("Hierarchy");
    editor.DrawInspector();
    ImGui.End();
    profiler.EndProfile("Hierarchy Window");

    // Update profiler (will create its own window)
    profiler.Update(Raylib.GetFrameTime());

    rlImGui.End();
    scene.Draw();
    Raylib.EndDrawing();

    profiler.EndProfile("Frame Time");
}

profiler.Dispose();
rlImGui.Shutdown();
