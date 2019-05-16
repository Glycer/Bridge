using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze2DSquare
{
    public bool isOutlet = false;

    public GameObject layout;
    public Maze2DSquare[] adjacents { get; set; }

    public int xCoord, yCoord;

    public Maze2DSquare(int x, int y)
    {
        xCoord = x;
        yCoord = y;
    }
}