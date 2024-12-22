using ImGuiNET;
using System.Numerics;

/// <summary>
/// Provides a simple dropdown UI element using ImGui.
/// Used for creating collapsible sections in the editor interface.
/// </summary>
public class UI_Dropdown {
    private bool expandMenu = false;

    /// <summary>
    /// Creates a button that toggles a dropdown menu.
    /// </summary>
    /// <param name="name">The text to display on the button.</param>
    /// <returns>True if the dropdown is expanded, false otherwise.</returns>
    public bool isButtonPressed(string name) {
        if (ImGui.Button(name, new Vector2(ImGui.GetWindowSize().X, 20))) {
            expandMenu = !expandMenu;
        }
        return expandMenu;
    }
}
