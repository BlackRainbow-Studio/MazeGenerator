using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeCreation
{
	/// <summary>
	/// Алгоритм создает коридоры из рандомной точки и присоединяет к прокопанным, если коридор соединяется с ними.
	/// Если коридор уходит в рамку, пропускает его.
	/// </summary>
	public class Wilsons : MapGenerator
	{
		List<MatrixPosition> notUsed = new List<MatrixPosition>();

		internal override void UseAlgorithm()
		{
			// create a starting cell
			int x = Random.Range(1, _width - 1);
			int z = Random.Range(1, _depth - 1);
			_map[x, z] = 2;

			int loop = 0;
			while (GetAvailableCells() > 1 && loop < 5000)
			{
				RandomWalk();
				loop++;
			}
		}

		int GetAvailableCells()
		{
			notUsed.Clear();
			for (int z = 1; z < _depth - 1; z++)
				for (int x = 1; x < _width - 1; x++)
					if (CountSquareMazeNeighbours(x, z) == 0)
						notUsed.Add(new MatrixPosition(x, z));

			return notUsed.Count;
		}

		void RandomWalk()
		{
			List<MatrixPosition> inWalk = new List<MatrixPosition>();
			bool validPath = false;

			int cx;
			int cz;
			int rstartIndex = Random.Range(0, notUsed.Count);
			cx = notUsed[rstartIndex].x;
			cz = notUsed[rstartIndex].z;
			inWalk.Add(new MatrixPosition(cx, cz));

			int loop = 0;
			while (cx > 0 && cx <= _width - 1 && cz > 0 && cz <= _depth - 1 && loop < 5000 && !validPath)
			{
				//Debug.Log("loop: " + loop);
				_map[cx, cz] = 0;
				if (CountSquareMazeNeighbours(cx, cz) > 1)
					break;

				int rDirection = Random.Range(0, directions.Count);
				//Debug.Log($"direction: { directions[rDirection].x}:{ directions[rDirection].z}");
				int nx = cx + directions[rDirection].x;
				int nz = cz + directions[rDirection].z;
				//Debug.Log($"nx, nz: {nx}:{nz}");

				if (CountSquareEmptyNeighbours(nx, nz) < 2)
				{
					cx = nx;
					cz = nz;
					inWalk.Add(new MatrixPosition(cx, cz));
				}
				validPath = (CountSquareMazeNeighbours(cx, cz) == 1);

				loop++;
			}

			if (validPath)
			{
				_map[cx, cz] = 0;
				//Debug.Log("PathFound");

				foreach (MatrixPosition m in inWalk)
				{
					_map[m.x, m.z] = 2;
				}
				inWalk.Clear();
			}
			else
			{
				foreach (MatrixPosition m in inWalk)
				{
					_map[m.x, m.z] = 1;
				}
				inWalk.Clear();
			}
		}
	}
}