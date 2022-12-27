using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Health health;
    public Coins coins;

    [Header("Movement")]
    public PlayerStat Speed;

    [Header("Attack")]
    public PlayerStat Damage;
    public PlayerStat AttackSpeed;
    public PlayerStat CritChance;

    [Header("Defense")]
    public PlayerStat Armor;
    public PlayerStat DodgeChance;
    public readonly float MaxDodgeChance = 0.6f;

    [Header("Healing")]
    public PlayerStat MaxHealth;
    public PlayerStat LifeSteal;

    [Header("Currency")]
    public PlayerStat Looting;

    private Dictionary<string, PlayerStat> statsDict;

    private void Awake()
    {
        statsDict = new()
        {
            { "movementspeed", Speed },
            { "damage", Damage },
            { "attackspeed", AttackSpeed },
            { "critchance", CritChance },
            { "armor", Armor },
            { "dodgechance", DodgeChance },
            { "maxhealth", MaxHealth },
            { "lifesteal", LifeSteal },
            { "looting", Looting },
        };
    }


    public void AddModifierToStat(PlayerStat stat)
    {
        var mod = new StatModifier(0.2f, StatModType.PercentAdd);
        stat.AddModifier(mod);
    }


    public PlayerStat GetStatOfName(string name)
    {
        name = string.Concat(name.Where(c => !char.IsWhiteSpace(c))).ToLower();
        return statsDict[name];
    }

    public void SetMaxHealth()
    {
        health.Initial = MaxHealth.Value;
    }
}
