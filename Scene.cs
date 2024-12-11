using Raylib_cs;
namespace Engine {
public class Scene {
  private List<GameObject> gameObjects = new List<GameObject>();
  private Camera2D _activeCamera2D;
  public Camera2D activeCamera2D {
    get { return _activeCamera2D; }
    set { _activeCamera2D = value; }
  }
  public void AddGameObject<T>(T gameObject)
      where T : GameObject {
    if (gameObject is Camera) {
      activeCamera2D = (gameObject as Camera).cameraComponent;
    }
    gameObjects.Add(gameObject);
  }

  public void RemoveGameObject(GameObject gameObject) {
    gameObjects.Remove(gameObject);
  }

  private void Draw() {
    // NOTE: we want our camera Component to send a signal about which one is
    // active.
    foreach (GameObject gameObject in gameObjects) {
      gameObject.Draw(activeCamera);
    }
  }

  private void DrawInspector() {
    foreach (GameObject gameObject in gameObjects) {
      gameObject.DrawInspector();
    }
  }

  private void EarlyUpdate() {
    foreach (GameObject gameObject in gameObjects) {
      gameObject.EarlyUpdate();
    }
  }

  private void Update() {
    foreach (GameObject gameObject in gameObjects) {
      gameObject.Update();
    }
  }

  private void LateUpdate() {
    foreach (GameObject gameObject in gameObjects) {
      gameObject.LateUpdate();
    }
  }
}

}
