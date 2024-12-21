using Raylib_cs;
using ImGuiNET;
using rlImGui_cs;
using System.Numerics;
using Engine;

Raylib.InitWindow(1000, 1000,
                  "Camera2D component system");
rlImGui.Setup(true);
var io = ImGui.GetIO();
io.ConfigFlags |= ImGuiConfigFlags.DockingEnable;
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

EditorUI editorUI = new EditorUI();

while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.RayWhite);

    rlImGui.Begin();
    editorUI.Draw();
    rlImGui.End();
    scene.Draw();
    Raylib.EndDrawing();
}

profiler.Dispose();
rlImGui.Shutdown();
