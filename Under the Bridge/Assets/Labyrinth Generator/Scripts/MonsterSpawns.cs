using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawns : MonoBehaviour
{
    public GameObject[] monsterType;
    public int[] numMonsters;

    public List<GameObject> monsters;

    void Start()
    {
        int j;
        for (int i = 0; i < monsterType.Length; i++)
        {
            for (j = 0; j < numMonsters[i]; j++)
            {
                monsters.Add(Instantiate(monsterType[i], transform));
            }
        }
    }
}
