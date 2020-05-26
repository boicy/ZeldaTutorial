using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public Powerup thisLoot;
    public int lootChance;

}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;

    public Powerup LootPowerup()
    {
        int cumProbability = 0;
        int currentProbability = UnityEngine.Random.Range(0, 100);

        foreach (Loot v in loots)
        {
            cumProbability += v.lootChance;
            if (currentProbability <= cumProbability)
            {
                return v.thisLoot;
            }
        }
        return null;
    }
}