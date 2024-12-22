using Raylib_cs;
namespace Engine
{
    /// <summary>
    /// Manages the game scene using the Singleton pattern. Handles the collection of root-level game objects
    /// and coordinates their lifecycle methods including updates, drawing, and inspector functionality.
    /// </summary>
    public sealed class Scene
    {
        private Scene() { }
        private static readonly object _lock = new object();
        private static Scene? instance = null;

        /// <summary>
        /// Gets the singleton instance of the Scene class, creating it if it doesn't exist.
        /// Thread-safe implementation using double-check locking pattern.
        /// </summary>
        public static Scene Instance
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new Scene();
                    }
                    return instance;
                }
            }
        }

        private List<GameObject> rootGameObjects = new List<GameObject>();

        /// <summary>
        /// The active camera used for rendering the scene.
        /// </summary>
        public Camera2D activeCamera;

        /// <summary>
        /// Gets or sets the list of root-level game objects in the scene.
        /// </summary>
        public List<GameObject> RootGameObjects
        {
            get => rootGameObjects;
            set => rootGameObjects = value;
        }

        /// <summary>
        /// Adds a game object to the scene if it has no parent.
        /// </summary>
        /// <typeparam name="T">Type of game object to add.</typeparam>
        /// <param name="gameObject">The game object to add to the scene.</param>
        public void AddGameObject<T>(T gameObject) where T : GameObject
        {
            if (gameObject is null)
            {
                return;
            }
            else if (gameObject.Parent is null)
            {
                rootGameObjects.Add(gameObject);
            }
        }

        /// <summary>
        /// Removes a game object from the scene's root objects.
        /// </summary>
        /// <param name="gameObject">The game object to remove.</param>
        public void RemoveGameObject(GameObject gameObject)
        {
            RootGameObjects.Remove(gameObject);
        }

        /// <summary>
        /// Draws all root game objects in the scene using the active camera.
        /// </summary>
        public void Draw()
        {
            foreach (GameObject gameObject in rootGameObjects)
            {
                gameObject.Draw(activeCamera);
            }
        }

        /// <summary>
        /// Draws the inspector interface for all root game objects.
        /// </summary>
        public void DrawInspector()
        {
            foreach (GameObject gameObject in RootGameObjects)
            {
                gameObject.DrawInspector();
            }
        }

        /// <summary>
        /// Performs early update operations on all root game objects.
        /// </summary>
        public void EarlyUpdate()
        {
            foreach (GameObject gameObject in RootGameObjects)
            {
                gameObject.EarlyUpdate();
            }
        }

        /// <summary>
        /// Performs main update operations on all root game objects.
        /// </summary>
        public void Update()
        {
            foreach (GameObject gameObject in RootGameObjects)
            {
                gameObject.Update();
            }
        }

        /// <summary>
        /// Performs late update operations on all root game objects.
        /// </summary>
        public void LateUpdate()
        {
            foreach (GameObject gameObject in RootGameObjects)
            {
                gameObject.LateUpdate();
            }
        }
    }
}
