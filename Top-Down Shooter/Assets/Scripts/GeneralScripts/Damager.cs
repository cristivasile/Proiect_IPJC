using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damager : MonoBehaviour
{
    [SerializeField]
    private float _damageAmount;

    protected void ApplyDamage(IDamageable damageable)
    {
        damageable.Damage(_damageAmount);
    }
}
