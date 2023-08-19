using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRoom : MonoBehaviour
{
    public SphereCollider roomArea;
    List<MazeSquare> squares;
    List<GameObject> monsters;

    public void InitMazeRoom(List<MazeSquare> newSquares, List<MonsterStats> monsterTypes, List<double> monsterRarity)
    {
        squares = newSquares;
        // Determine room size
        int lowestX = squares[0].xCoord;
        int lowestY = squares[0].yCoord;
        int lowestZ = squares[0].zCoord;
        int greatestX = squares[0].xCoord;
        int greatestY = squares[0].yCoord;
        int greatestZ = squares[0].zCoord;
        foreach (MazeSquare square in squares)
        {
            if (square.xCoord < lowestX)
                lowestX = square.xCoord;
            else if (square.xCoord > greatestX)
                greatestX = square.xCoord;
            if (square.yCoord < lowestY)
                lowestY = square.yCoord;
            else if (square.yCoord > greatestY)
                greatestY = square.yCoord;
            if (square.zCoord < lowestZ)
                lowestZ = square.zCoord;
            else if (square.zCoord > greatestZ)
                greatestZ = square.zCoord;
        }
        int greatestDistance = 0;
        if (greatestX - lowestX > greatestDistance)
            greatestDistance = greatestX - lowestX;
        if (greatestY - lowestY > greatestDistance)
            greatestDistance = greatestY - lowestY;
        if (greatestZ - lowestZ > greatestDistance)
            greatestDistance = greatestZ - lowestZ;
        roomArea.radius = greatestDistance;

        FillMonsters(monsterTypes, monsterRarity, (double)(greatestX - lowestX + greatestY - lowestY));
    }
    void FillMonsters(List<MonsterStats> monsterTypes, List<double> monsterRarity, double roomSpace)
    {
        System.Random random = new System.Random();
        double totalRarity = 0;
        double lowestRarity = 65535;
        foreach (double rarity in monsterRarity)
        {
            totalRarity += rarity;
            if (rarity < lowestRarity)
                lowestRarity = rarity;
        }
        double randDouble;
        int i;
        while (roomSpace >= lowestRarity)
        {
            randDouble = random.NextDouble() * totalRarity;
            for (i = 0; i < monsterRarity.Count; i++)
            {
                randDouble -= monsterRarity[i];
                if (randDouble <= 0 && roomSpace >= monsterTypes[i].GetWeight())
                {
                    roomSpace -= monsterTypes[i].GetWeight();
                    monsters.Add(Instantiate(monsterTypes[i].gameObject));
                }
            }
            // Infinite loop avoidance
            roomSpace -= 0.001f;
        }
    }

    void OnTriggerEnter()
    {
        System.Random random = new System.Random();
        MazeSquare currSquare = squares[random.Next(0, squares.Count)];
        foreach (GameObject monster in monsters)
        {
            if (!monster.GetComponent<MonsterStats>().IsDead())
                monster.GetComponent<MonsterStats>().Spawn(new Vector3(currSquare.xCoord, currSquare.yCoord, currSquare.zCoord));
        }
    }
    void OnTriggerExit()
    {
        foreach (GameObject monster in monsters)
        {
            if (!monster.GetComponent<MonsterStats>().IsDead())
                monster.GetComponent<MonsterStats>().DeSpawn();
        }
    }
}
