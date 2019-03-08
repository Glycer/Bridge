using UnityEngine;

public class MazeSquare
{
    public bool isOutlet = false;

    public GameObject layout;
    public MazeSquare[] adjacents { get; set; }

    public int xCoord, yCoord, zCoord;

    public MazeSquare(int x, int y, int z)
    {
        xCoord = x;
        yCoord = y;
        zCoord = z;
    }
}
