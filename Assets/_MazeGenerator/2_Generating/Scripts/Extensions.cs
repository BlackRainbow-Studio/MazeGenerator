using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace MazeCreation
{
    public static class Extensions
    {
        private static System.Random rng = new System.Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void Shuffle<T>(this Stack<T> stack)
        {
            var values = stack.ToArray();
            stack.Clear();
            foreach (var value in values.OrderBy(x => rng.Next()))
                stack.Push(value);
        }
    }

    public class PathPoint
    {
        public Vector2Int location;
        public int index;

        public PathPoint(Vector2Int l, int i)
        {
            location = l;
            index = i;
        }
    }
}