namespace Engine {
public class Editor2D {
  Editor2D() {}
  private static readonly object _lock = new object();
  private static Editor2D? instance = null;
  public static Editor2D Instance {
    get {
      lock (_lock) {
        if (instance == null) {
          instance = new Editor2D();
        }
        return instance;
      }
    }
  }
  private GameObject? selectedGameObject;
  public void DrawSelectedGameObjectsComponents() {
    if (selectedGameObject is not null) {
      foreach (Component component in selectedGameObject.GetComponents()) {
        if (component.dropdown.isButtonPressed(
                component.GetType().ToString().Replace("Engine.", ""))) {
          component.DrawInspector();
        }
      }
    }
  }
}
}
