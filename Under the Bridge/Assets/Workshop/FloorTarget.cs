using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTarget : MonoBehaviour
{
    public enum Direction { Down, North, South, East, West, Up };
    public Direction direction;

    public readonly Dictionary<Direction, Vector3> rotations = new Dictionary<Direction, Vector3>() {
        { Direction.Down, new Vector3(0, 0, 0) },
        { Direction.North, new Vector3(-90, 0, 0) },
        { Direction.South, new Vector3(90, 0, 0) }
    };

    public Transform player;

    private void OnMouseDown()
    {
        //StartCoroutine(GShift());
        player.localEulerAngles = rotations[direction];
    }

    IEnumerator GShift()
    {
        float delay = .1f;

        for (float f = 0; f < 1; f += delay)
        {
            player.localEulerAngles = Vector3.Slerp(player.localEulerAngles, rotations[direction], f);
            yield return new WaitForSeconds(delay);
        }

        player.localEulerAngles = Vector3.Slerp(player.localEulerAngles, rotations[direction], 1);
    }
}
