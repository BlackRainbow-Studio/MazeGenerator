using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tester : MonoBehaviour
{
    public MazeGenerator maze;
    public TextMesh text;
    
    public void SqaureEmptyNeighbours(int x, int z)
	{
         text.text = maze.CountSquareEmptyNeighbours(x, z).ToString();
	}
}
