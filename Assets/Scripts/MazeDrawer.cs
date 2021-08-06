using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MazeDrawer : MonoBehaviour
{
    private byte[,] map;

    
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
                else if (CheckSquarePattern(x, z, new byte[] { 0, 1, 0, 1 }))
                {
                    ceil_prefab = (GameObject)PrefabUtility.InstantiatePrefab(straight_Prefab);
                }
                // HALL horizontal
                else if (CheckSquarePattern(x, z, new byte[] { 1, 0, 1, 0 }))
                {
                    ceil_prefab = (GameObject)PrefabUtility.InstantiatePrefab(straight_Prefab);
                    ceil_prefab.transform.Rotate(Vector3.up, 90);
                }
                // CROSSROAD
                else if (CheckSquarePattern(x, z, new byte[] { 0, 0, 0, 0 }))
                {
                    ceil_prefab = (GameObject)PrefabUtility.InstantiatePrefab(cross_Prefab);
                }
                // CORNER up-right
                else if (CheckSquarePattern(x, z, new byte[] { 0, 0, 1, 1 }))
                {
                    ceil_prefab = (GameObject)PrefabUtility.InstantiatePrefab(rounded_Prefab);
                    ceil_prefab.transform.Rotate(Vector3.up, 90);
                }
                // CORNER right-down
                else if (CheckSquarePattern(x, z, new byte[] { 1, 0, 0, 1 }))
                {
                    ceil_prefab = (GameObject)PrefabUtility.InstantiatePrefab(rounded_Prefab);
                    ceil_prefab.transform.Rotate(Vector3.up, 180);
                }
                // CORNER down-left
                else if (CheckSquarePattern(x, z, new byte[] { 1, 1, 0, 0 }))
                {
                    ceil_prefab = (GameObject)PrefabUtility.InstantiatePrefab(rounded_Prefab);
                    ceil_prefab.transform.Rotate(Vector3.up, 270);
                }
                // CORNER left-up
                else if (CheckSquarePattern(x, z, new byte[] { 0, 1, 1, 0 }))
                {
                    ceil_prefab = (GameObject)PrefabUtility.InstantiatePrefab(rounded_Prefab);
                    //block.transform.Rotate(Vector3.up, 0);
                }
                // DEADEND up
                else if (CheckSquarePattern(x, z, new byte[] { 1, 1, 0, 1 }))
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

    public bool CheckSquarePattern(int x, int z, byte[] pattern)
    {
        int check = 0;

        if (map[x, z + 1] == pattern[0]) check++;
        if (map[x + 1, z] == pattern[1]) check++;
        if (map[x, z - 1] == pattern[2]) check++;
        if (map[x - 1, z] == pattern[3]) check++;

        return (check == 4);
    }
}
