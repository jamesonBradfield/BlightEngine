using Raylib_cs;
namespace Engine {
public sealed class Scene {
  Scene() {}
  private static readonly object _lock = new object();
  private static Scene? instance = null;
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

  public List<GameObject> GameObjects {
    get => gameObjects;
    set => gameObjects = value;
  }

  public void AddGameObject<T>(T gameObject)
      where T : GameObject {
    if (gameObject is null) {
      return;
    }
    gameObjects.Add(gameObject);
  }
  public void RemoveGameObject(GameObject gameObject) {
    GameObjects.Remove(gameObject);
  }

  public void Draw() {}

  public void DrawInspector() {
    foreach (GameObject gameObject in GameObjects) {
      gameObject.DrawInspector();
    }
  }

  public void EarlyUpdate() {
    foreach (GameObject gameObject in GameObjects) {
      gameObject.EarlyUpdate();
    }
  }

  public void Update() {
    foreach (GameObject gameObject in GameObjects) {
      gameObject.Update();
    }
  }

  public void LateUpdate() {
    foreach (GameObject gameObject in GameObjects) {
      gameObject.LateUpdate();
    }
  }
}

}
