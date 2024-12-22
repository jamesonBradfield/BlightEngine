// Component.cs
using Raylib_cs;
namespace Engine
{
    /// <summary>
    /// Base class for all components in the engine. Provides core functionality and lifecycle methods
    /// that can be overridden by derived components.
    /// </summary>
    public class Component
    {
        /// <summary>
        /// UI dropdown control used for component inspection in the editor.
        /// </summary>
        public UI_Dropdown dropdown = new();

        /// <summary>
        /// Draws the component from a 2D camera perspective.
        /// </summary>
        /// <param name="camera">The 2D camera to render from.</param>
        public virtual void Draw(Camera2D camera) { }

        /// <summary>
        /// Draws the component from a 3D camera perspective.
        /// </summary>
        /// <param name="camera">The 3D camera to render from.</param>
        public virtual void Draw(Camera3D camera) { }

        /// <summary>
        /// Draws the component's inspector interface in the editor.
        /// </summary>
        public virtual void DrawInspector() { }

        /// <summary>
        /// Called before the regular Update method in the component lifecycle.
        /// </summary>
        public virtual void EarlyUpdate() { }

        /// <summary>
        /// Called every frame during the main update phase.
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// Called after the regular Update method in the component lifecycle.
        /// </summary>
        public virtual void LateUpdate() { }

        /// <summary>
        /// Called when the component is first initialized.
        /// </summary>
        public virtual void Initialize() { }
    }
}
