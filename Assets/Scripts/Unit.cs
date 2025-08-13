using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour  // Like template for characters
{
    public string unitName;
    public int damage;
    public int maxHP;
    public int currentHP;

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)  // To know if dead
            return true;
        else
            return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;

        if (currentHP >= maxHP)  // Don't exceed maxHP
            currentHP = maxHP;
    }
}
