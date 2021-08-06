using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recursive : MazeGenerator
{
	public override void GenerateMap()
	{
		GenerateMap(Random.Range(1, width-1), Random.Range(1, depth-1));
	}

	void GenerateMap(int x, int z)
	{
		if (CountSquareEmptyNeighbours(x, z) >= 2) return;
		map[x, z] = 0;

		directions.Shuffle();
		GenerateMap(x + directions[0].x, z + directions[0].z);
		GenerateMap(x + directions[1].x, z + directions[1].z);
		GenerateMap(x + directions[2].x, z + directions[2].z);
		GenerateMap(x + directions[3].x, z + directions[3].z);
	}
}
