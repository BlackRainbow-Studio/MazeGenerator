using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeCreation
{
    public class PropsSpawner : MonoBehaviour
    {
        [Header("Props prefab")]
        public GameObject fps;
        public GameObject finish_Prefab;

        /*private void PositionPlayer(Maze maze)
        {
            for (int x = 0; x < maze._width; x++)
                for (int z = 0; z < maze._depth; z++)
                {
                    if (maze.map[x, z] == 0)
                    {
                        fps.transform.position = new Vector3(x * maze._scale, 0, z * maze._scale);
                        return;
                    }
                }
        }*/
        /*    private void SpawnFinish()
            {
                for (int x = width-1; x > 0; x--)
                    for (int z = depth-1; z > 0; z--)
                    {
                        if (map[x, z] == 0)
                        {
                            if (!CheckSquarePattern(x, z, new int[] { 0, 0, 1, 1 }) &&
                                !CheckSquarePattern(x, z, new int[] { 1, 0, 0, 1 }) &&
                                !CheckSquarePattern(x, z, new int[] { 1, 1, 0, 0 }) &&
                                !CheckSquarePattern(x, z, new int[] { 0, 1, 1, 0 }))
                            {
                                GameObject fin = Instantiate(finish_Prefab, new Vector3(x * scale, 0, z * scale), Quaternion.identity, transform);
                                return;
                            }

                        }
                    }
                Debug.Log($"Finish spawned.");
            }*/

    }
}