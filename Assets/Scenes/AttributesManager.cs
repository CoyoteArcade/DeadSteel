using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesManager : MonoBehaviour
{
    //public means thatb these variables can be accessed or modified by outside scripts
    public int health;
    public int attack;
    public int armor;
    public float critDamage = 1.5f;
    public float critChance = 0.5f;

    public void TakeDamage(int amount)
    {
        health -= amount - (amount * armor/100);
    }

    public void DealDamage(GameObject target)
    {
        var atm = target.GetComponent<AttributesManager>();
        if(atm != null)
        {
            float totalDamage = attack;

            //count RNG chance
            if(Random.Range(0f, 1f) < critChance)
            {
                totalDamage *= critDamage;
            }
            atm.TakeDamage(attack);
        }
    }
}
