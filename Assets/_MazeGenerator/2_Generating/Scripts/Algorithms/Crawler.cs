using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeCreation
{
	public class Crawler : MapGenerator
	{
		internal override void UseAlgorithm()
		{
			for (int i = 0; i < 3; i++)
				CrawlH();
			for (int i = 0; i < 2; i++)
				CrawlV();
		}

		static void CrawlV()
		{
			bool done = false;
			int x = Random.Range(1, _width - 1);
			int z = 1;

			while (!done)
			{
				_map[x, z] = 0;
				if (Random.Range(0, 100) < 50)
					x += 1 - 2 * Random.Range(0, 2);
				else
					z += 1 - Random.Range(0, 2);
				done |= (x < 1 || x >= _width - 1 || z < 1 || z >= _depth - 1);
			}
		}
		static void CrawlH()
		{
			bool done = false;
			int x = 1;
			int z = Random.Range(1, _depth - 1);

			while (!done)
			{
				_map[x, z] = 0;
				if (Random.Range(0, 100) < 50)
					x += 1 - Random.Range(0, 2);
				else
					z += 1 - 2 * Random.Range(0, 2);
				done |= (x < 1 || x >= _width - 1 || z < 1 || z >= _depth - 1);
			}
		}
	}
}