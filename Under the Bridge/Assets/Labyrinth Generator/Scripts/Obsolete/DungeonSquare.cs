using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSquare
{
    public const int ID = 1000;

    public bool isOutlet = false;

    public GameObject layout;
    public DungeonSquare[] adjacents { get; set; }

    public int xCoord, yCoord, zCoord;
    public int id;

    public DungeonSquare (int x, int y)
    {
        xCoord = x;
        yCoord = y;

        id = x * ID + y;
    }

    public DungeonSquare(int x, int y, int z)
    {
        xCoord = x;
        yCoord = y;
        zCoord = z;

        id = x * ID + y * ID + z;
    }
}