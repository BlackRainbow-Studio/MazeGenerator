using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MazeCreation
{
    public enum Algorithms
    {
        Crawler,
        Prims,
        Recursive,
        Wilsons
    }

    public class Spawner : MonoBehaviour
    {
        [Header("Initial settings")]
        public int width = 15; // x length
        public int depth = 15; // z length
        public int scale = 3;
        public Algorithms algorithm;

        private Drawer drawer;
        private MapGenerator mapGenerator;

        public int[,] matrix; // private?
        private Maze maze;

        public void InitializeTool()
		{
            drawer = GetComponent<Drawer>();

            switch(algorithm)
			{
                case Algorithms.Crawler:
                    mapGenerator = new Crawler();
                    break;
                case Algorithms.Prims:
                    mapGenerator = new Prims();
                    break;
                case Algorithms.Recursive:
                    mapGenerator = new Recursive();
                    break;
                case Algorithms.Wilsons:
                    mapGenerator = new Wilsons();
                    break;
            }
		}

        public void CreateNewMaze()
        {
            maze = new GameObject("Maze").AddComponent<Maze>();
            maze.transform.SetParent(transform);

            InitializeMatrix();

        maze.map = mapGenerator.GenerateMap(matrix);

            drawer.DrawMap(maze, scale);
        }

        void InitializeMatrix()
        {
            matrix = new int[width, depth];
            for (int z = 0; z < depth; z++)
                for (int x = 0; x < width; x++)
                {
                    matrix[x, z] = 1; // 1=wall, 0=corridor
                }

            maze.map = matrix;
            Debug.Log($"Matrix[{width},{depth}] initialized");
        }

        public void ClearScene()
        {
            while (transform.childCount > 0)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }

            Debug.ClearDeveloperConsole();
        }

        // ------TESTING------
        public void ShowMapInConsole()
		{
            for (int z=0; z<depth; z++)
			{
                string row = $"row  {z}: ";
                for (int x=0; x<width; x++)
				{
                    row += $"[{maze.map[x,z]}]";
				}
                Debug.Log(row);
			}
		}
    }
}