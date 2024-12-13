using Raylib_cs;
using ImGuiNET;
using rlImGui_cs;
using Engine;
Raylib.InitWindow(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2,
                  "Camera2D component system");
rlImGui.Setup(true);
Scene scene = Scene.Instance;
scene.AddGameObject(new Camera2DGameObject("Camera", null, null));
while (!Raylib.WindowShouldClose()) {
  Raylib.BeginDrawing();
  Raylib.ClearBackground(Color.Gray);
  rlImGui.Begin();
  ImGui.Begin("window");
  scene.DrawHierarchy();
  rlImGui.End();
  Raylib.EndDrawing();
}
rlImGui.Shutdown(); // cleans up ImGui
