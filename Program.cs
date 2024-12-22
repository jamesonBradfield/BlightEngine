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
// Create camera with centered view
Camera2DGameObject cam = new();
cam.cameraComponent.camera2D.Target = Vector2.Zero;  // Look at center
cam.cameraComponent.camera2D.Zoom = 1.0f;  // Normal zoom
scene.AddGameObject(cam);
scene.activeCamera = cam.cameraComponent.camera2D;

// Create square at a visible position
var squareTransform = new Transform2D(
    Position: new Vector2(0, 0),  // Center position
    Rotation: 0.0f,
    Scale: Vector2.One
);
GameObject square = new GameObject(cam, squareTransform);
square.AddComponent<SquareRendererComponent2D>(new SquareRendererComponent2D(100, Color.Black));  // Smaller size to start
scene.AddGameObject(square);

EditorUI editorUI = new EditorUI();

while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.RayWhite);

    rlImGui.Begin();
    editorUI.Draw();
    rlImGui.End();
    Raylib.EndDrawing();
}
editorUI.Dispose();
rlImGui.Shutdown();
