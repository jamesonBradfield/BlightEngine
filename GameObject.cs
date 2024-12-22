using Raylib_cs;
namespace Engine
{
    /// <summary>
    /// Represents a game object in the scene hierarchy. Manages components, parent-child relationships,
    /// and provides core functionality for game object behavior and rendering.
    /// </summary>
    public class GameObject
    {
        /// <summary>
        /// The name of the game object, used for identification in the editor.
        /// </summary>
        public string name;

        /// <summary>
        /// Indicates if the game object is currently selected in the editor.
        /// </summary>
        protected bool selected = false;

        /// <summary>
        /// Indicates if the game object's hierarchy is expanded in the editor.
        /// </summary>
        protected bool expanded = false;

        private List<GameObject> _Children = new List<GameObject>();
        private GameObject? _Parent;
        private List<Component> components = new List<Component>();

        /// <summary>
        /// Gets or sets the child game objects of this object.
        /// Updates transform hierarchy when modified.
        /// </summary>
        public List<GameObject>? Children
        {
            get => _Children;
            set
            {
                Transform.SetChildren(value);
                _Children = value;
            }
        }

        /// <summary>
        /// Gets or sets the parent game object.
        /// Updates transform hierarchy when modified.
        /// </summary>
        public GameObject? Parent
        {
            get => _Parent;
            set
            {
                Transform.SetParent(value);
                _Parent = value;
            }
        }

        /// <summary>
        /// Gets or sets the transform component that handles positioning and hierarchy.
        /// </summary>
        public Transform2D Transform
        {
            get => _Transform;
            set => _Transform = value;
        }

        private Transform2D? _Transform;

        /// <summary>
        /// Initializes a new game object with a parent and transform.
        /// </summary>
        /// <param name="Parent">The parent game object.</param>
        /// <param name="Transform">The initial transform for this object.</param>
        public GameObject(GameObject Parent, Transform2D Transform)
        {
            this.name = "GameObject";
            this.Transform = Transform;
            this.Parent = Parent;
            this.Parent.Children?.Add(this);
            this.AddComponent<Transform2D>(Transform);
        }

        /// <summary>
        /// Initializes a new game object with just a transform.
        /// </summary>
        /// <param name="Transform">The initial transform for this object.</param>
        public GameObject(Transform2D Transform)
        {
            this.name = "GameObject";
            this.Transform = Transform;
            this.Parent = null;
            this.AddComponent<Transform2D>(Transform);
        }

        /// <summary>
        /// Initializes a new game object with default settings.
        /// </summary>
        public GameObject()
        {
            this.name = "GameObject";
            this.Transform = new Transform2D();
            this.Parent = null;
            this.AddComponent<Transform2D>(Transform);
        }

        /// <summary>
        /// Gets the first component of type T attached to this game object.
        /// </summary>
        /// <typeparam name="T">The type of component to retrieve.</typeparam>
        /// <returns>The requested component.</returns>
        /// <exception cref="SystemException">Thrown when the component is not found.</exception>
        public Component GetComponent<T>() where T : Component
        {
            try
            {
                Component component = components.Find(x => x is T);
                return component;
            }
            catch
            {
                throw new SystemException("Component not found");
            }
        }

        /// <summary>
        /// Gets all components attached to this game object.
        /// </summary>
        /// <returns>A list of all attached components.</returns>
        public List<Component> GetComponents() { return components; }

        /// <summary>
        /// Adds a component to this game object.
        /// </summary>
        /// <typeparam name="T">The type of component to add.</typeparam>
        /// <param name="component">The component instance to add.</param>
        public virtual void AddComponent<T>(T component) where T : Component
        { components.Add(component); }

        // Lifecycle methods with documentation
        /// <summary>
        /// Initializes the game object and all its components.
        /// </summary>
        public virtual void Initialize()
        {
            foreach (Component component in components)
            {
                component.Initialize();
            }
        }

        /// <summary>
        /// Draws the game object and its components using a 2D camera.
        /// </summary>
        /// <param name="camera">The camera to use for rendering.</param>
        public virtual void Draw(Camera2D camera)
        {
            foreach (Component component in components)
            {
                component.Draw(camera);
            }
        }

        /// <summary>
        /// Draws the game object and all its children using a 3D camera.
        /// </summary>
        /// <param name="camera">The camera to use for rendering.</param>
        public virtual void Draw(Camera3D camera)
        {
            DrawComponents(camera);
            foreach (GameObject child in Children)
            {
                child.Draw(camera);
            }
        }

        private void DrawComponents(Camera3D camera)
        {
            foreach (Component component in components)
            {
                component.Draw(camera);
            }
        }

        /// <summary>
        /// Draws the inspector interface for this game object.
        /// </summary>
        public virtual void DrawInspector() { }

        /// <summary>
        /// Called at the start of each frame before regular updates.
        /// </summary>
        public virtual void EarlyUpdate()
        {
            foreach (Component component in components)
            {
                component.EarlyUpdate();
            }
        }

        /// <summary>
        /// Called each frame for regular updates.
        /// </summary>
        public virtual void Update()
        {
            foreach (Component component in components)
            {
                component.Update();
            }
        }

        /// <summary>
        /// Called at the end of each frame after regular updates.
        /// </summary>
        public virtual void LateUpdate()
        {
            foreach (Component component in components)
            {
                component.DrawInspector();
            }
        }
    }
}
