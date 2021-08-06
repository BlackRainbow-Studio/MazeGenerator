using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Crawler
{
	private static byte[,] map;
	private static int width;
	private static int depth;
	public static byte[,] GenerateMap(byte[,] mapDraft)
	{
		map = mapDraft;

		width = map.GetLength(0);
		depth = map.GetLength(1);

		for (int i=0; i<3; i++) 
			CrawlH();
		for (int i = 0; i < 2; i++) 
			CrawlV();

		return map;
	}

	static void CrawlV()
	{
		bool done = false;
		int x = Random.Range(1, width-1);
		int z = 1;

		while (!done)
		{
			map[x, z] = 0;
			if (Random.Range(0, 100) < 50)
				x += 1 - 2*Random.Range(0, 2);
			else
				z += 1 - Random.Range(0, 2);
			done |= (x < 1 || x >= width - 1 || z < 1 || z >= depth - 1);
		}
	}
	static void CrawlH()
	{
		bool done = false;
		int x = 1;
		int z = Random.Range(1, depth-1);

		while (!done)
		{
			map[x, z] = 0;
			if (Random.Range(0, 100) < 50)
				x += 1 - Random.Range(0, 2);
			else
				z += 1 - 2*Random.Range(0, 2);
			done |= (x < 1 || x >= width - 1 || z < 1 || z >= depth - 1);
		}
	}
}
