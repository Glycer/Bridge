using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprawlerSquare
{
    public bool isOutlet = false;

    public GameObject layout;
    public SprawlerSquare[] adjacents { get; set; }

    public Vector3 coordinates;

    public SprawlerSquare(Vector3 coords)
    {
        coordinates = coords;
    }
}
