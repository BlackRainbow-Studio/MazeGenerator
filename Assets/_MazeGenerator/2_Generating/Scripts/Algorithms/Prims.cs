using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeCreation
{
	/// <summary>
	/// Копает коридоры
	/// </summary>
	public class Prims : MapGenerator
	{
		internal override void UseAlgorithm()
		{
			int x = 2;
			int z = 2;
			_map[x, z] = 0;

			// начинаем формировать список доступных для копания ячеек
			List<MatrixPosition> walls = new List<MatrixPosition>();
			walls.Add(new MatrixPosition(x - 1, z));
			walls.Add(new MatrixPosition(x + 1, z));
			walls.Add(new MatrixPosition(x, z - 1));
			walls.Add(new MatrixPosition(x, z + 1));

			int countloops = 0; // предохранитель от бесконечного цикла
			while (walls.Count > 0 && countloops < 5000) // пока есть хоть одна стена
			{
				int rwall = Random.Range(0, walls.Count); // берем любую, убираем из списка стен и делаем активной
				x = walls[rwall].x;
				z = walls[rwall].z;
				walls.RemoveAt(rwall);

				// копаем ячейку, только если у нее ровно 1 пустая соседняя, то есть это новый коридор
				// добавляем новые доступные для копания (включая предыдущую?)
				if (CountSquareEmptyNeighbours(x, z) == 1)
				{
					_map[x, z] = 0;
					walls.Add(new MatrixPosition(x - 1, z));
					walls.Add(new MatrixPosition(x + 1, z));
					walls.Add(new MatrixPosition(x, z - 1));
					walls.Add(new MatrixPosition(x, z + 1));
				}
				//else Debug.Log(CountSquareEmptyNeighbours(x, z).ToString());

				countloops++;
			}
		}
	}
}