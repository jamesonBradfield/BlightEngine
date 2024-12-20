using ImGuiNET;
using System.Numerics;
public class UI_Dropdown {
  private bool expandMenu = false;
  public bool isButtonPressed(string name) {
    if (ImGui.Button(name, new Vector2(ImGui.GetWindowSize().X, 20))) {
      expandMenu = !expandMenu;
    }
    return expandMenu;
  }
}
