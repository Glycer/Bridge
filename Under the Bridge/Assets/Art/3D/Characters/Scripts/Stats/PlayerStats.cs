using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static UIManager UI;

    public static int playerStrength = 1;
    static int playerMaxHealth = 100;
    static float playerHealth;
    static int[] playerMaxMana;
    static float[] playerMana;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = playerMaxHealth;
        playerMaxMana = new int[3];
        playerMaxMana[0] = 100;
        playerMaxMana[1] = 100;
        playerMaxMana[2] = 100;
        playerMana = new float[3];
        playerMana[0] = 0;
        playerMana[1] = 0;
        playerMana[2] = 0;
    }

    // Use negative to heal
    static public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth > playerMaxHealth)
            playerHealth = playerMaxHealth;
        if (playerHealth < 0)
            playerHealth = 0;
        UI.AdjustStatus(0, playerHealth / playerMaxHealth);
    }
    // 0 is blood, 1 is water, 2 is light, use negative amount to subtract
    static public void AddMana(int manaIndex, int amount)
    {
        playerMana[manaIndex] += amount;
        if (playerMana[manaIndex] > playerMaxMana[manaIndex])
            playerMana[manaIndex] = playerMaxMana[manaIndex];
        if (playerMana[manaIndex] < 0)
            playerMana[manaIndex] = 0;
        UI.AdjustStatus(manaIndex + 1, playerMana[manaIndex] / playerMaxMana[manaIndex]);
    }
}
