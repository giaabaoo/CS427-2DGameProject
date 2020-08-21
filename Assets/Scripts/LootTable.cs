using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot
{
    //public Powerup thisLoot;
    public int lootChance;
}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;
}
