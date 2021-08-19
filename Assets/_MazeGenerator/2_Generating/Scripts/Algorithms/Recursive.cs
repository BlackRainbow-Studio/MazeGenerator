using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeCreation
{
	public class Recursive : MapGenerator
	{
		internal override void UseAlgorithm()
		{
			Step(Random.Range(1, _width - 1), Random.Range(1, _depth - 1));
		}

		void Step(int x, int z)
		{
			if (CountSquareEmptyNeighbours(x, z) >= 2) return;

			_map[x, z] = 0;

			directions.Shuffle();
			Step(x + directions[0].x, z + directions[0].z);
			Step(x + directions[1].x, z + directions[1].z);
			Step(x + directions[2].x, z + directions[2].z);
			Step(x + directions[3].x, z + directions[3].z);
		}
	}
}