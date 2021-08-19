using UnityEngine;
using UnityEditor;

namespace MazeCreation
{
    [CustomEditor(typeof(Spawner))]
    public class SpawnerGUI : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Spawner generator = (Spawner)target;

            if (GUILayout.Button("Initialize tool"))
                generator.InitializeTool();
            if (GUILayout.Button("Create"))
                generator.CreateNewMaze();
            if (GUILayout.Button("Show map to console"))
                generator.ShowMapInConsole();
            if (GUILayout.Button("Delete all mazes"))
                generator.ClearScene();
        }
    }
}