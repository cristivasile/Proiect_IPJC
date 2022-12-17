using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Healer : MonoBehaviour
{
    [SerializeField]
    private float _healAmount;

    protected void ApplyHeal(IHealable healable)
    {
        healable.Heal(_healAmount);
    }
}
