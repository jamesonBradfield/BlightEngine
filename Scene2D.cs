using Raylib_cs;
namespace Engine {
public sealed class Scene {
  Scene() {}
  private static readonly object _lock = new object();
  private static Scene instance = null;
  public static Scene Instance {
    get {
      lock (_lock) {
        if (instance == null) {
          instance = new Scene();
        }
        return instance;
      }
    }
  }
  private List<GameObject> gameObjects = new List<GameObject>();
  private Camera2D? _activeCamera2D;
  public Camera2D? activeCamera2D {
    get { return _activeCamera2D; }
    set { _activeCamera2D = value; }
  }

  public List<GameObject> GameObjects {
    get => gameObjects;
    set => gameObjects = value;
  }

  private Camera2DGameObject camera2DGameObject;

  public void AddGameObject<T>(T gameObject)
      where T : GameObject {
    if (gameObject is not null) {
      if (gameObject is Camera2DGameObject) {
        Camera2DGameObject? cameraGameObject =
            (gameObject as Camera2DGameObject);
        if (cameraGameObject is not null &&
            cameraGameObject.cameraComponent is not null) {
          camera2DGameObject = cameraGameObject;
          _activeCamera2D = cameraGameObject.cameraComponent.camera2D;
        }
      }
      GameObjects.Add(gameObject);
    }
  }
  public void DrawHierarchy() {
    foreach (GameObject gameObject in gameObjects) {
      gameObject.DrawInspector();
    }
  }
  public void RemoveGameObject(GameObject gameObject) {
    GameObjects.Remove(gameObject);
  }

  private void Draw() {
    if (_activeCamera2D is not null) {
      camera2DGameObject.Draw(_activeCamera2D);
    }
  }

  private void DrawInspector() {
    foreach (GameObject gameObject in GameObjects) {
      gameObject.DrawInspector();
    }
  }

  private void EarlyUpdate() {
    foreach (GameObject gameObject in GameObjects) {
      gameObject.EarlyUpdate();
    }
  }

  private void Update() {
    foreach (GameObject gameObject in GameObjects) {
      gameObject.Update();
    }
  }

  private void LateUpdate() {
    foreach (GameObject gameObject in GameObjects) {
      gameObject.LateUpdate();
    }
  }
}

}
