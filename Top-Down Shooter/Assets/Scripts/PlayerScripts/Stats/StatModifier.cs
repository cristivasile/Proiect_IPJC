using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatModType
{
    Flat,
    PercentAdd,
    PercentMult
}

[Serializable]
public class StatModifier
{
    public readonly float Value;
    public readonly StatModType Type;
    public readonly int Order;

    public StatModifier(float value, StatModType type, int order)
    {
        Value = value;
        Type = type;
        Order = order;
    }

    public StatModifier(float value, StatModType type) : this (value, type, (int)type)
    {

    }
}
