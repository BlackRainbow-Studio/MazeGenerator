using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MazeCreation
{
	public class Drawer : MonoBehaviour
	{
		[Header("Modules prefabs")]
		public GameObject wall_Prefab;

		public GameObject rounded_Prefab;
		public GameObject corner_Prefab;
		public GameObject cross_Prefab;
		public GameObject deadend_Prefab;
		public GameObject straight_Prefab;
		public GameObject T_Prefab;

		private int[,] _map;
		private int _width; // x length
		private int _depth; // z length
		private int _scale;

		public void DrawMap(Maze maze, int scale)
		{
			_map = maze.map;
			_width = _map.GetLength(0);
			_depth = _map.GetLength(1);
			_scale = scale;

			Debug.Log("Drawing maze..");
			maze.transform.localPosition = Vector3.zero;

			for (int z = 0; z < _depth; z++)
				for (int x = 0; x < _width; x++)
				{
					Vector3 positionInMap = new Vector3(x * _scale, 0, z * _scale);

					GameObject ceil = new GameObject($"{x.ToString()},{z.ToString()}");
					ceil.transform.SetParent(maze.transform);
					ceil.transform.localPosition = positionInMap;

					GameObject model_prefab;
					int rotation = 0;

					#region Define pattern cases
					// wall
					if (_map[x, z] == 1)
					{
						if (wall_Prefab != null)
							model_prefab = (wall_Prefab);
						else
							continue;
					}
					// HALL vertical
					else if (CheckSquarePattern(x, z, new int[] { 0, 1, 0, 1 }))
					{
						model_prefab = (straight_Prefab);
					}
					// HALL horizontal
					else if (CheckSquarePattern(x, z, new int[] { 1, 0, 1, 0 }))
					{
						model_prefab = (straight_Prefab);
						rotation = 90;
					}
					// CROSSROAD
					else if (CheckSquarePattern(x, z, new int[] { 0, 0, 0, 0 }))
					{
						model_prefab = (cross_Prefab);
					}
					// CORNER up-right
					else if (CheckSquarePattern(x, z, new int[] { 0, 0, 1, 1 }))
					{
						model_prefab = (rounded_Prefab);
						rotation = 270;
					}
					// CORNER right-down
					else if (CheckSquarePattern(x, z, new int[] { 1, 0, 0, 1 }))
					{
						model_prefab = (rounded_Prefab);
						rotation = 0;
					}
					// CORNER down-left
					else if (CheckSquarePattern(x, z, new int[] { 1, 1, 0, 0 }))
					{
						model_prefab = (rounded_Prefab);
						rotation = 90;
					}
					// CORNER left-up
					else if (CheckSquarePattern(x, z, new int[] { 0, 1, 1, 0 }))
					{
						model_prefab = (rounded_Prefab);
						rotation = 180;
					}
					// DEADEND up
					else if (CheckSquarePattern(x, z, new int[] { 1, 1, 0, 1 }))
					{
						model_prefab = (deadend_Prefab);
					}
					// DEADEND right
					else if (CheckSquarePattern(x, z, new int[] { 1, 1, 1, 0 }))
					{
						model_prefab = (deadend_Prefab);
						rotation = 90;
					}
					// DEADEND down
					else if (CheckSquarePattern(x, z, new int[] { 0, 1, 1, 1 }))
					{
						model_prefab = (deadend_Prefab);
						rotation = 180;
					}
					// DEADEND left
					else if (CheckSquarePattern(x, z, new int[] { 1, 0, 1, 1 }))
					{
						model_prefab = (deadend_Prefab);
						rotation = 270;
					}
					// T right
					else if (CheckSquarePattern(x, z, new int[] { 0, 0, 0, 1 }))
					{
						model_prefab = (T_Prefab);
					}
					// T down
					else if (CheckSquarePattern(x, z, new int[] { 1, 0, 0, 0 }))
					{
						model_prefab = (T_Prefab);
						rotation = 90;
					}
					// T left
					else if (CheckSquarePattern(x, z, new int[] { 0, 1, 0, 0 }))
					{
						model_prefab = (T_Prefab);
						rotation = 180;
					}
					// T up
					else if (CheckSquarePattern(x, z, new int[] { 0, 0, 1, 0 }))
					{
						model_prefab = (T_Prefab);
						rotation = 270;						
					}
					else
					{
						model_prefab = null;
					}
					#endregion

					if (model_prefab)
					{
						GameObject model = (GameObject)PrefabUtility.InstantiatePrefab(model_prefab);
						model.name = $"{model_prefab.name}_{ceil.name}";
						model.transform.SetParent(ceil.transform);
						model.transform.localPosition = Vector3.zero;
						model.transform.Rotate(Vector3.up, rotation);
					}
				}
		}

		public bool CheckSquarePattern(int x, int z, int[] pattern)
		{
			int check = 0;

			if (_map[x, z + 1] == pattern[0]) check++;
			if (_map[x + 1, z] == pattern[1]) check++;
			if (_map[x, z - 1] == pattern[2]) check++;
			if (_map[x - 1, z] == pattern[3]) check++;

			return (check == 4);
		}
	}
}