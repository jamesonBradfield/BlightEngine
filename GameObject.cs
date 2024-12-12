using Raylib_cs;
namespace Engine {
public class GameObject {
  public string name;
  private List<GameObject> _Children = new List<GameObject>();
  private GameObject? _Parent;
  private List<Component> components = new List<Component>();

  public List<GameObject> Children {
    get => _Children;
    set => _Children = value;
  }

  public GameObject? Parent {
    get => _Parent;
    set => _Parent = value;
  }

  public Transform2D Transform {
    get => _Transform;
    set => _Transform = value;
  }
  private Transform2D _Transform;

  public GameObject(string? name, GameObject? Parent, Transform2D? Transform) {
    if (name is not null) {
      this.name = name;
    } else {
      this.name = "GameObject";
    }
    if (Parent is not null) {
      this.Parent = Parent;
    } else {
      this.Parent = null;
    }
    if (Transform is not null) {
      this.Transform = Transform;
    } else {
      this.Transform = new Transform2D(null, null, null);
    }
  }

  public Component GetComponent<T>()
      where T : Component {
    try {
      Component component = components.Find(x => x is T);
      component.gameObject = this;
      return component;
    } catch {
      throw new SystemException("Component not found");
    }
  }

  public virtual void AddComponent<T>(T component)
      where T : Component {
    component.gameObject = this;
    components.Add(component);
  }

  public virtual void Initialize() {
    foreach (Component component in components) {
      component.Initialize();
    }
  }

  public virtual void Draw(Camera2D? camera) {
    foreach (Component component in components) {
      component.Draw(camera);
    }
  }

  public virtual void Draw(Camera3D? camera) {
    foreach (Component component in components) {
      component.Draw(camera);
    }
  }

  public virtual void DrawInspector() {
    foreach (Component component in components) {
      component.DrawInspector();
    }
  }

  public virtual void EarlyUpdate() {
    foreach (Component component in components) {
      component.EarlyUpdate();
    }
  }

  public virtual void Update() {
    foreach (Component component in components) {
      component.Update();
    }
  }

  public virtual void LateUpdate() {
    foreach (Component component in components) {
      component.DrawInspector();
    }
  }
}
}
