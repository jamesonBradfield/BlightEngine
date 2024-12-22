using Raylib_cs;
namespace Engine
{
    public sealed class Scene
    {
        Scene() { }
        private static readonly object _lock = new object();
        private static Scene? instance = null;
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
        public Camera2D activeCamera;
        public List<GameObject> RootGameObjects
        {
            get => rootGameObjects;
            set => rootGameObjects = value;
        }

        public void AddGameObject<T>(T gameObject)
            where T : GameObject
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
        public void RemoveGameObject(GameObject gameObject)
        {
            RootGameObjects.Remove(gameObject);
        }

        public void Draw()
        {
            foreach (GameObject gameObject in rootGameObjects)
            {
                gameObject.Draw(activeCamera);
            }
        }

        public void DrawInspector()
        {
            foreach (GameObject gameObject in RootGameObjects)
            {
                gameObject.DrawInspector();
            }
        }

        public void EarlyUpdate()
        {
            foreach (GameObject gameObject in RootGameObjects)
            {
                gameObject.EarlyUpdate();
            }
        }

        public void Update()
        {
            foreach (GameObject gameObject in RootGameObjects)
            {
                gameObject.Update();
            }
        }

        public void LateUpdate()
        {
            foreach (GameObject gameObject in RootGameObjects)
            {
                gameObject.LateUpdate();
            }
        }
    }

}
