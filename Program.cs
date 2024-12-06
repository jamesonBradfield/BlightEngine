using Raylib_cs;
using ImGuiNET;
using rlImGui_cs;

Raylib.InitWindow(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2,
                  "Camera2D component system");
// before your game loop
rlImGui.Setup(true); // sets up ImGui with ether a dark or light default theme
while (!Raylib.WindowShouldClose()) {
  Raylib.BeginDrawing();
  Raylib.ClearBackground(Color.Gray);
  // inside your game loop, between BeginDrawing() and EndDrawing()
  rlImGui.Begin(); // starts the ImGui content mode. Make all ImGui calls after
                   // this
  ImGui.Begin("window");

  rlImGui
      .End(); // ends the ImGui content mode. Make all ImGui calls before this
  Raylib.EndDrawing();
}
// after your game loop is over, before you close the window

rlImGui.Shutdown(); // cleans up ImGui
