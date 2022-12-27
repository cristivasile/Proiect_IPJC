using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

[Serializable]
public class PlayerStat
{
    public float BaseValue;
    public float Value
    {
        get
        {
            if(hasChanged)
            {
                _value = FinalValue();
                hasChanged = false;
            }
            return _value;
        }
    }
    private bool hasChanged = true;
    [SerializeField]
    private float _value;

    [SerializeField]
    private readonly List<StatModifier> statModifiers;

    public PlayerStat()
    {
        statModifiers = new List<StatModifier>();
    }

    public PlayerStat(float baseValue) : this()
    {
        BaseValue = baseValue;
    }

    private int CompareModOrder(StatModifier a, StatModifier b)
    {
        if (a.Order < b.Order)
            return -1;
        else if (a.Order > b.Order)
            return 1;
        return 0;
    }

    public void AddModifier(StatModifier mod)
    {
        hasChanged = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModOrder);
    }

    public bool RemoveModifier()
    {
        if (statModifiers.Remove(statModifiers.LastOrDefault()))
        {
            hasChanged = true;
            return true;
        }
        return false;
    }
    public bool RemoveModifier(StatModifier mod)
    {
        if (statModifiers.Remove(mod))
        {
            hasChanged = true;
            return true;
        }
        return false;
    }

    public float FinalValue()
    {
        float finalValue = BaseValue;
        float sumPercentAdd = 0f;

        for (int i = 0; i < statModifiers.Count; i++)
        {
            var mod = statModifiers[i];
            switch (mod.Type)
            {
                case StatModType.Flat:
                    finalValue += mod.Value;
                    break;

                case StatModType.PercentAdd:
                    sumPercentAdd += mod.Value;
                    if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
                    {
                        finalValue *= 1f + sumPercentAdd;
                        sumPercentAdd = 0f;
                    }
                    break;

                case StatModType.PercentMult:
                    finalValue *= 1f + mod.Value;
                    break;
            }
        }

        return (float)Mathf.Round(finalValue);
    }
}
