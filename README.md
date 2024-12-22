# About
this is a Unity-Like Raylib C# engine I'm building for architecture practice, it uses rlImGui-cs and ImGuiNET along with Raylib-cs. Currently i am still yet to display anything.
![screenshot of the editor in its current form](media/2024-12-22-145239_hyprshot.png)
## Features
- Hierarchy
- Transform2D
- Component System
- Dockable ImGui Windows
## Installation
```bash
touch your_project_name
cd your_project_name
dotnet new console
dotnet add package rlImGui-cs ImGuiNET Raylib-cs
```
## TODO
- second revision of rendererComponent2D, Transform2D, and hierarchy systems as they work together.
- seperate editor logic and scene logic better.
