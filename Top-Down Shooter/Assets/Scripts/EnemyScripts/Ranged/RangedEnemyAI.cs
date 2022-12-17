using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAI : EnemyAI
{
    private new void Start()
    {
        base.Start();

        chaseRange = 16f;
        attackRange = 5f;
        attackDelay = 1.5f;
        passedTime = attackDelay;
    }
}
