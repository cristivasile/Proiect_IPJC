using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 100f;
    public float unitSpeed = 5f;

    [Header("Attack")]
    public float damage = 100f;
    public float atackSpeed = 100f;
    public float critChance = 10f;

    [Header("Defense")]
    public float armor = 0f;
    public float dodgeChance = 10f;
    public float maxDodgeChance = 60f;

    [Header("Healing")]
    public float lifeSteal = 0f;

    [Header("Currency")]
    public float looting = 0f;
}
