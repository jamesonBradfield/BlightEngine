using ImGuiNET;

namespace Engine
{
    /// <summary>
    /// Editor2D provides a graphical user interface for manipulating game objects and components in a 2D scene.
    /// Implements the Singleton pattern to ensure only one editor instance exists.
    /// </summary>
    public class Editor2D
    {
        // Private constructor to prevent direct instantiation (part of Singleton pattern)
        Editor2D() { }

        // Lock object for thread-safe singleton implementation
        private static readonly object _lock = new object();

        // Singleton instance field
        private static Editor2D? instance = null;

        /// <summary>
        /// Singleton instance property implementing double-check locking pattern for thread safety
        /// </summary>
        public static Editor2D Instance
        {
            get
            {
                lock (_lock) // Thread-safe lock
                {
                    if (instance == null)
                    {
                        instance = new Editor2D();
                    }
                    return instance;
                }
            }
        }

        // Reference to currently selected game object in the editor
        private GameObject? selectedGameObject;

        /// <summary>
        /// Draws the component inspector UI for the currently selected game object.
        /// Each component is displayed with a dropdown button and its inspector interface.
        /// </summary>
        public void DrawSelectedGameObjectsComponents()
        {
            if (selectedGameObject is not null)
            {
                // Iterate through all components on the selected object
                foreach (Component component in selectedGameObject.GetComponents())
                {
                    // Create a dropdown button for each component
                    // Removes "Engine." namespace prefix from component type name
                    if (component.dropdown.isButtonPressed(
                            component.GetType().ToString().Replace("Engine.", "")))
                    {
                        // Draw the component's inspector UI if dropdown is open
                        component.DrawInspector();
                    }
                }
            }
        }

        // Reference to the singleton scene instance
        private Scene scene = Scene.Instance;

        // Base ImGui tree node flags for consistent tree behavior
        // OpenOnArrow: Allows expanding/collapsing with arrow button
        // OpenOnDoubleClick: Allows expanding/collapsing with double click
        // SpanAvailWidth: Makes the clickable area span the full width
        private ImGuiTreeNodeFlags baseFlags = ImGuiTreeNodeFlags.OpenOnArrow |
                                             ImGuiTreeNodeFlags.OpenOnDoubleClick |
                                             ImGuiTreeNodeFlags.SpanAvailWidth;

        /// <summary>
        /// Draws the main hierarchy inspector window showing all game objects in the scene.
        /// Handles object selection and hierarchy visualization.
        /// </summary>
        public void DrawInspector()
        {
            // Clear selection when clicking empty space in the window
            if (ImGui.IsWindowHovered() && ImGui.IsMouseClicked(ImGuiMouseButton.Left))
            {
                selectedGameObject = null;
            }

            // Draw each root-level game object in the scene
            foreach (GameObject gameObject in scene.GameObjects)
            {
                DrawGameObjectNode(gameObject);
            }
        }

        /// <summary>
        /// Recursively draws a game object and all its children in the hierarchy tree.
        /// Handles selection state, visual styling, and tree node behavior.
        /// </summary>
        /// <param name="gameObject">The game object to draw in the hierarchy</param>
        private void DrawGameObjectNode(GameObject gameObject)
        {
            // Start with base tree node flags
            ImGuiTreeNodeFlags flags = baseFlags;

            // Add selected flag if this is the currently selected object
            if (selectedGameObject == gameObject)
            {
                flags |= ImGuiTreeNodeFlags.Selected;
            }

            // Add leaf flag if this object has no children
            // Leaf nodes are displayed differently (no expand arrow)
            if (gameObject.Children == null || gameObject.Children.Count == 0)
            {
                flags |= ImGuiTreeNodeFlags.Leaf;
            }

            // Create tree node with unique ID (using object's hash code)
            // '###' prefix in ImGui creates an ID without visible label
            bool nodeOpen = ImGui.TreeNodeEx($"###{gameObject.GetHashCode()}", flags);

            // Handle selection when clicking the tree node
            if (ImGui.IsItemClicked(ImGuiMouseButton.Left))
            {
                selectedGameObject = gameObject;
            }

            // Draw the object's name on the same line as the tree node
            ImGui.SameLine();
            ImGui.Text(gameObject.name);

            // If node is expanded, recursively draw all child objects
            if (nodeOpen)
            {
                if (gameObject.Children != null)
                {
                    foreach (GameObject child in gameObject.Children)
                    {
                        DrawGameObjectNode(child);
                    }
                }
                // End the tree node (must be called for each TreeNodeEx)
                ImGui.TreePop();
            }
        }
    }
}
