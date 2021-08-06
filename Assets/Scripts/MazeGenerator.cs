using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapLocation
{
    public int x;
    public int z;

    public MapLocation(int _x, int _z)
	{
        x = _x;
        z = _z;
	}
}
public class MazeGenerator : MonoBehaviour
{
    // заменить на Dictionary?
    public List<MapLocation> directions = new List<MapLocation> {
                                            new MapLocation(1,0), // вправо
											new MapLocation(0,1), // вверх
											new MapLocation(-1,0), // влево
											new MapLocation(0,-1)}; //вниз
    public int width = 15; // x length
    public int depth = 15; // z length
    public byte[,] map;
    public int scale = 6;

    [Header("Module prefabs")]
    public GameObject wall_Prefab;
    public GameObject straight_Prefab;
    public GameObject T_Prefab;
    public GameObject rounded_Prefab;
    public GameObject corner_Prefab;
    public GameObject cross_Prefab;
    public GameObject deadend_Prefab;
    [Header("Props prefab")]
    public GameObject fps;
    public GameObject finish_Prefab;

    [Header("Maze root prefab")]
    public GameObject maze_prefab;
    internal GameObject Maze { get; set; }

    void Start()
    {
        //Restart();
    }
    [ContextMenu("New maze")]
    public void CreateNewMaze()
	{
        InitializeMap();
        GenerateMap();
        //DrawMap();
    }
    void InitializeMap()
    {
        map = new byte[width, depth];
        for (int z = 0; z < depth; z++)
            for (int x = 0; x < width; x++)
            {
                map[x, z] = 1; // 1=wall, 0=corridor
            }
    }
    public virtual void GenerateMap()
    {
        // (GameObject)PrefabUtility.InstantiatePrefab(maze_prefab);
        Crawler.GenerateMap(map);
    }
/*    void DrawMap()
    {
        Debug.Log("Creating new maze root..");
        Maze = new GameObject($"Maze");
        Maze.transform.SetParent(transform);
        Maze.transform.localPosition = Vector3.zero;

        for (int z = 0; z < depth; z++)
            for (int x = 0; x < width; x++)
            {
                Vector3 positionInMap = new Vector3(x * scale, 0, z * scale);

                GameObject ceil = new GameObject($"{x.ToString()},{z.ToString()}");
                ceil.transform.SetParent(Maze.transform);
                ceil.transform.localPosition = positionInMap;

                GameObject ceil_prefab;
                
				#region Define pattern cases
				// wall
				if (map[x, z] == 1)
                {
                    ceil_prefab = (GameObject)PrefabUtility.InstantiatePrefab(wall_Prefab);
                }
                // HALL vertical
                else if (CheckSquarePattern(x,z, new byte[] {0,1,0,1})) 
                {
                    ceil_prefab = (GameObject)PrefabUtility.InstantiatePrefab(straight_Prefab);
                }
                // HALL horizontal
                else if (CheckSquarePattern(x, z, new byte[] {1,0,1,0})) 
                {
                    ceil_prefab = (GameObject)PrefabUtility.InstantiatePrefab(straight_Prefab);
                    ceil_prefab.transform.Rotate(Vector3.up, 90);
                }
                // CROSSROAD
                else if (CheckSquarePattern(x, z, new byte[] {0,0,0,0}))
                {
                    ceil_prefab = (GameObject)PrefabUtility.InstantiatePrefab(cross_Prefab);
                }
                // CORNER up-right
                else if (CheckSquarePattern(x, z, new byte[] {0,0,1,1}))
                {
                    ceil_prefab = (GameObject)PrefabUtility.InstantiatePrefab(rounded_Prefab);
                    ceil_prefab.transform.Rotate(Vector3.up, 90);
                }
                // CORNER right-down
                else if (CheckSquarePattern(x, z, new byte[] {1,0,0,1}))
                {
                    ceil_prefab = (GameObject)PrefabUtility.InstantiatePrefab(rounded_Prefab);
                    ceil_prefab.transform.Rotate(Vector3.up, 180);
                }
                // CORNER down-left
                else if (CheckSquarePattern(x, z, new byte[] {1,1,0,0}))
                {
                    ceil_prefab = (GameObject)PrefabUtility.InstantiatePrefab(rounded_Prefab);
                    ceil_prefab.transform.Rotate(Vector3.up, 270);
                }
                // CORNER left-up
                else if (CheckSquarePattern(x, z, new byte[] {0,1,1,0}))
                {
                    ceil_prefab = (GameObject)PrefabUtility.InstantiatePrefab(rounded_Prefab);
                    //block.transform.Rotate(Vector3.up, 0);
                }
                // DEADEND up
                else if (CheckSquarePattern(x, z, new byte[] { 1, 1, 0, 1}))
                {
                    ceil_prefab = (GameObject)PrefabUtility.InstantiatePrefab(deadend_Prefab);
                    ceil_prefab.transform.Rotate(Vector3.up, 360);
                }
                // DEADEND right
                else if (CheckSquarePattern(x, z, new byte[] { 1, 1, 1, 0 }))
                {
                    ceil_prefab = (GameObject)PrefabUtility.InstantiatePrefab(deadend_Prefab);
                    ceil_prefab.transform.Rotate(Vector3.up, 90);
                }
                // DEADEND down
                else if (CheckSquarePattern(x, z, new byte[] { 0, 1, 1, 1 }))
                {
                    ceil_prefab = (GameObject)PrefabUtility.InstantiatePrefab(deadend_Prefab);
                    ceil_prefab.transform.Rotate(Vector3.up, 180);
                }
                // DEADEND left
                else if (CheckSquarePattern(x, z, new byte[] { 1, 0, 1, 1 }))
                {
                    ceil_prefab = (GameObject)PrefabUtility.InstantiatePrefab(deadend_Prefab);
                    ceil_prefab.transform.Rotate(Vector3.up, 270);
                }
                // T right
                else if (CheckSquarePattern(x, z, new byte[] { 0, 0, 0, 1 }))
                {
                    ceil_prefab = (GameObject)PrefabUtility.InstantiatePrefab(T_Prefab);
                    ceil_prefab.transform.Rotate(Vector3.up, 360);
                }
                // T down
                else if (CheckSquarePattern(x, z, new byte[] { 1, 0, 0, 0 }))
                {
                    ceil_prefab = (GameObject)PrefabUtility.InstantiatePrefab(T_Prefab);
                    ceil_prefab.transform.Rotate(Vector3.up, 90);
                }
                // T left
                else if (CheckSquarePattern(x, z, new byte[] { 0, 1, 0, 0 }))
                {
                    ceil_prefab = (GameObject)PrefabUtility.InstantiatePrefab(T_Prefab);
                    ceil_prefab.transform.Rotate(Vector3.up, 180);
                }
                // T up
                else if (CheckSquarePattern(x, z, new byte[] { 0, 0, 1, 0 }))
                {
                    ceil_prefab = (GameObject)PrefabUtility.InstantiatePrefab(T_Prefab);
                    ceil_prefab.transform.Rotate(Vector3.up, 270);
                }
                else
				{
                    ceil_prefab = null;
				}
				#endregion

				if (ceil_prefab)
                {
                    ceil_prefab.name = $"{ceil_prefab.name}_{ceil.name}";
                    ceil_prefab.transform.SetParent(ceil.transform);
                    ceil_prefab.transform.localPosition = Vector3.zero;
                }
            }
    }*/
    private byte[] DefinePattern(int x, int z) // пока не работает, да и не нужно до генерации подземелий. нужно попробовать SequenceEquals для сравнения массивов
	{
        byte[] pattern = new byte[9];
        for (int i=0; i<9; i++)
		{
            switch (i)
            {
                case 0:
                    pattern[i] = map[x-1, z+1];
                    break;
                case 1:
                    pattern[i] = map[x, z+1];
                    break;
                case 2:
                    pattern[i] = map[x+1, z+1];
                    break;
                case 3:
                    pattern[i] = map[x-1, z];
                    break;
                case 4:
                    pattern[i] = map[x, z];
                    break;
                case 5:
                    pattern[i] = map[x+1, z];
                    break;
                case 6:
                    pattern[i] = map[x-1, z-1];
                    break;
                case 7:
                    pattern[i] = map[x, z-1];
                    break;
                case 8:
                    pattern[i] = map[x+1, z-1];
                    break;
            }
        }

        //Debug.Log($"Pattern {x},{z}: {pattern[0]},{pattern[1]},{pattern[2]},{pattern[3]},{pattern[4]},{pattern[5]},{pattern[6]},{pattern[7]},{pattern[8]}");
        return pattern;
	}
    private bool CheckPattern(int x, int z, byte[] pattern)
	{
        int count = 0;
        int pos = 0;
        for (int row=1; row >-2;row--)
		{
            for (int column = -1; column < 2; column++)
            {
                if (pattern[pos] == map[x + column, z + row] || pattern[pos] == 5)
                    count++;
                pos++;
			}
		}

        return (count == 9);
	}
    public int CountSquareEmptyNeighbours(int x, int z)
	{
        int count = 0;
        if (x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1) return 5;
        if (map[x - 1, z] == 0) count++;
        if (map[x + 1, z] == 0) count++;
        if (map[x, z - 1] == 0) count++;
        if (map[x, z + 1] == 0) count++;
        return count;
    }
    /// <summary>
    /// альтернатива DefinePattern для лабиринтов без "комнат"
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <param name="pattern"></param>
    /// <returns></returns>

    public int CountDiagonalEmptyNeighbours(int x, int z)
    {
        int count = 0;
        if (x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1) return 5;
        if (map[x - 1, z - 1] == 0) count++;
        if (map[x + 1, z + 1] == 0) count++;
        if (map[x - 1, z + 1] == 0) count++;
        if (map[x + 1, z - 1] == 0) count++;
        return count;
    }
    public int CountAllEmptyNeighbours(int x, int z)
    {
        return CountSquareEmptyNeighbours(x, z) + CountDiagonalEmptyNeighbours(x, z);
    }
    private void PositionPlayer() 
    {
        for (int x=0; x<width;x++)
            for (int z=0; z<depth;z++)
			{
                if (map[x,z]==0)
				{
                    fps.transform.position = new Vector3(x * scale, 0, z * scale);
                    return;
				}
			}
    }
/*    private void SpawnFinish()
    {
        for (int x = width-1; x > 0; x--)
            for (int z = depth-1; z > 0; z--)
            {
                if (map[x, z] == 0)
                {
                    if (!CheckSquarePattern(x, z, new byte[] { 0, 0, 1, 1 }) &&
                        !CheckSquarePattern(x, z, new byte[] { 1, 0, 0, 1 }) &&
                        !CheckSquarePattern(x, z, new byte[] { 1, 1, 0, 0 }) &&
                        !CheckSquarePattern(x, z, new byte[] { 0, 1, 1, 0 }))
                    {
                        GameObject fin = Instantiate(finish_Prefab, new Vector3(x * scale, 0, z * scale), Quaternion.identity, transform);
                        return;
                    }
                    
                }
            }
        Debug.Log($"Finish spawned.");
    }*/

    [ContextMenu("Clear scene")]
    void ClearScene()
    {
        DestroyImmediate(Maze);
    }
}
