using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Edge
{
    public MapLocation gridRef { get; set; }
    public int Cell1 { get; set; }
    public int Cell2 { get; set; }
    public MapLocation loc1 { get; set; }
    public MapLocation loc2 { get; set; }
}

public class Kruskal : MazeGenerator
{
    List<MapLocation> directions = new List<MapLocation>() { new MapLocation(1,0),
                                                   new MapLocation(0,1)};

    public override void GenerateMap()
    {
        Stack<Edge> edges = new Stack<Edge>();
        List<int> vertices = new List<int>();

        //create all edges
        for (int x = 2; x < width - 2; x += 2)
        {
            int n = Random.Range(0, 2);
            //use flattened 2d map cell indexes
            edges.Push(new Edge()
            {
                gridRef = new MapLocation(x, 1)
            });
        }
        for (int z = 2; z < depth - 2; z += 2)
        {
            for (int x = 1; x < width - 2; x += 2)
            {
                int n = Random.Range(0, 2);
                //use flattened 2d map cell indexes
                edges.Push(new Edge()
                {
                    gridRef = new MapLocation(x, z)

                });
                //Debug.Log("cells: " + edges.Peek().Cell1 + " " + edges.Peek().Cell2);
                //UpdateMap(x, z, 0);
            }
            z++;
            for (int x = 2; x < width-2; x += 2)
            {
                int n = Random.Range(0, 2);
                //use flattened 2d map cell indexes
                edges.Push(new Edge()
                {
                    gridRef = new MapLocation(x, z)
                });
                //Debug.Log("cells: " + edges.Peek().Cell1 + " " + edges.Peek().Cell2);
                //UpdateMap(x, z, 0);
            }
        }

        for (int z = 1; z < depth; z+=1)
            for (int x = 1; x < width; x+=1)
                vertices.Add(x + z * depth);

        Kruskals(edges, vertices);
        /*foreach (Edge edge in MinimumSpanningTree)
        {
            UpdateMap(edge.loc1.x, edge.loc1.z, 0);
            UpdateMap(edge.loc2.x, edge.loc2.z, 0);
        }*/
    }

    void Kruskals(Stack<Edge> edges, List<int> vertices)
    {
        //empty result list
        //Stack<Edge> result = new Stack<Edge>();

        //making set
        DisjointSet.Set set = new DisjointSet.Set(width*depth);
        foreach (int vertex in vertices)
        {
            set.MakeSet(vertex);
            //Debug.Log("vertex: " + vertex);
        }

        edges.Shuffle();

        foreach(Edge edge in edges)
        {
            MapLocation cell1;
            MapLocation cell2;
            if (Random.Range(0,2) < 1)
            {
                cell1 = new MapLocation(edge.gridRef.x + 1, edge.gridRef.z);
                cell2 = new MapLocation(edge.gridRef.x - 1, edge.gridRef.z);
            }
            else
            {
                cell1 = new MapLocation(edge.gridRef.x, edge.gridRef.z + 1);
                cell2 = new MapLocation(edge.gridRef.x, edge.gridRef.z - 1);
            }

            //adding edge to result if both vertices do not belong to same set
            //both vertices in same set means it can have cycles in tree
            //Debug.Log("Find: " + (cell1.x + cell1.z * depth));
            if (set.FindSet(cell1.x + cell1.z * depth) != set.FindSet(cell2.x + cell2.z * depth))
            {
                set.Union(cell1.x + cell1.z * depth, cell2.x + cell2.z * depth);
                set.Union(cell1.x + cell1.z * depth, edge.gridRef.x + edge.gridRef.z * depth);
                /* Yasna: next 3 methods were commented because of "Assets\Kruskal.cs(112,17): error CS0103: The name 'UpdateMap' does not exist in the current context
" error
                UpdateMap(cell1.x, cell1.z, 0);
                UpdateMap(cell2.x, cell2.z, 0);
                UpdateMap(edge.gridRef.x, edge.gridRef.z, 0);
                */
            }
            else
            {
                Debug.Log("match");
            }

        }
    }
}
