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
    static int[] playerMana;
    // Used by Wyatt
    static public bool defending;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = playerMaxHealth;
        playerMaxMana = new int[3];
        playerMaxMana[0] = 100;
        playerMaxMana[1] = 100;
        playerMaxMana[2] = 100;
        playerMana = new int[3];
        playerMana[0] = 0;
        playerMana[1] = 0;
        playerMana[2] = 0;
    }

    // Use negative to heal
    static public void TakeDamage(int damage, bool blockable = false)
    {
        // Ignore if blockable and defending
        if (!(blockable && defending))
        {
            playerHealth -= damage;
            if (playerHealth > playerMaxHealth)
                playerHealth = playerMaxHealth;
            if (playerHealth < 0)
                playerHealth = 0;
            UI.AdjustStatus(0, playerHealth / playerMaxHealth);
        }
    }
    static public bool HealthFull()
    {
        return playerHealth == playerMaxHealth;
    }
    // 0 is blood, 1 is water, 2 is light, use negative amount to subtract
    static public void AddMana(int manaIndex, int amount)
    {
        playerMana[manaIndex] += amount;
        if (playerMana[manaIndex] > playerMaxMana[manaIndex])
            playerMana[manaIndex] = playerMaxMana[manaIndex];
        if (playerMana[manaIndex] < 0)
            playerMana[manaIndex] = 0;
        UI.AdjustStatus(manaIndex + 1, (float)playerMana[manaIndex] / playerMaxMana[manaIndex]);
    }
    static public bool CheckMana(int[] manaCost)
    {
        if (playerMana[0] - manaCost[0] >= 0 && playerMana[1] - manaCost[1] >= 0 && playerMana[2] - manaCost[2] >= 0)
            return true;
        else
            return false;
    }
    static public void SpendMana(int[] manaCost)
    {
        playerMana[0] -= manaCost[0];
        playerMana[1] -= manaCost[1];
        playerMana[2] -= manaCost[2];
        UI.AdjustStatus(1, playerMana[0] / playerMaxMana[0]);
        UI.AdjustStatus(2, playerMana[1] / playerMaxMana[1]);
        UI.AdjustStatus(3, playerMana[2] / playerMaxMana[2]);
    }
    static public void EmptyMana()
    {
        playerMana[0] = 0;
        playerMana[1] = 0;
        playerMana[2] = 0;
        UI.AdjustStatus(1, playerMana[0] / playerMaxMana[0]);
        UI.AdjustStatus(2, playerMana[1] / playerMaxMana[1]);
        UI.AdjustStatus(3, playerMana[2] / playerMaxMana[2]);
    }
    static public int GetMana(int manaIndex)
    {
        return playerMana[manaIndex];
    }
}
