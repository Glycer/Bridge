using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLoot : MonoBehaviour
{
    static int light;
    static int dark;
    static int life;
    static int death;
    static int fire;
    static int water;

    // Start is called before the first frame update
    void Start()
    {
        light = 0;
        dark = 0;
        life = 0;
        death = 0;
        fire = 0;
        water = 0;
    }

    public static void AddLight(int loot)
    {
        light += loot;
    }
    public static void AddDark(int loot)
    {
        dark += loot;
    }
    public static void AddLife(int loot)
    {
        life += loot;
    }
    public static void AddDeath(int loot)
    {
        death += loot;
    }
    public static void AddFire(int loot)
    {
        fire += loot;
    }
    public static void AddWater(int loot)
    {
        water += loot;
    }

    public static int GetLight()
    {
        return light;
    }
    public static int GetDark()
    {
        return dark;
    }
    public static int GetLife()
    {
        return life;
    }
    public static int GetDeath()
    {
        return death;
    }
    public static int GetFire()
    {
        return fire;
    }
    public static int GetWater()
    {
        return water;
    }
}
