using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawns : MonoBehaviour
{
    public GameObject monsterType;
    public int monstersNum;

    public List<GameObject> monsters;

    void Start()
    {
        for (int i = 0; i < monstersNum; i++)
        {
            monsters.Add(Instantiate(monsterType, transform));
        }
    }
}
