using ImGuiNET;
namespace Engine
{
    public class Editor2D
    {
        Editor2D() { }
        private static readonly object _lock = new object();
        private static Editor2D? instance = null;
        public static Editor2D Instance
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new Editor2D();
                    }
                    return instance;
                }
            }
        }
        private GameObject? selectedGameObject;
        public void DrawSelectedGameObjectsComponents()
        {
            if (selectedGameObject is not null)
            {
                foreach (Component component in selectedGameObject.GetComponents())
                {
                    if (component.dropdown.isButtonPressed(
                            component.GetType().ToString().Replace("Engine.", "")))
                    {
                        component.DrawInspector();
                    }
                }
            }
        }
        private Scene scene = Scene.Instance;

        private ImGuiTreeNodeFlags baseFlags = ImGuiTreeNodeFlags.OpenOnArrow |
                                             ImGuiTreeNodeFlags.OpenOnDoubleClick |
                                             ImGuiTreeNodeFlags.SpanAvailWidth;

        public void DrawInspector()
        {
            // Reset selection flag if clicking empty space
            if (ImGui.IsWindowHovered() && ImGui.IsMouseClicked(ImGuiMouseButton.Left))
            {
                selectedGameObject = null;
            }

            foreach (GameObject gameObject in scene.GameObjects)
            {
                DrawGameObjectNode(gameObject);
            }
        }

        private void DrawGameObjectNode(GameObject gameObject)
        {
            ImGuiTreeNodeFlags flags = baseFlags;

            if (selectedGameObject == gameObject)
            {
                flags |= ImGuiTreeNodeFlags.Selected;
            }

            if (gameObject.Children == null || gameObject.Children.Count == 0)
            {
                flags |= ImGuiTreeNodeFlags.Leaf;
            }

            bool nodeOpen = ImGui.TreeNodeEx($"###{gameObject.GetHashCode()}", flags);

            if (ImGui.IsItemClicked(ImGuiMouseButton.Left))
            {
                selectedGameObject = gameObject;
            }

            ImGui.SameLine();
            ImGui.Text(gameObject.name);

            if (nodeOpen)
            {
                if (gameObject.Children != null)
                {
                    foreach (GameObject child in gameObject.Children)
                    {
                        DrawGameObjectNode(child);
                    }
                }
                ImGui.TreePop();
            }
        }

    }
}
