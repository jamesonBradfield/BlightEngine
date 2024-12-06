using Raylib_cs;
using ImGuiNET;
using rlImGui_cs;
namespace Engine {
public class Scene {
  private Camera2D activeCamera2D;
  private Camera3D activeCamera3D;
  private List<Camera3D> cameras3D = new List<Camera3D>();
  private List<Camera2D> cameras2D = new List<Camera2D>();
}

}
