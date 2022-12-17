using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAI : EnemyAI
{
    private new void Start()
    {
        base.Start();

        chaseRange = 10f;
        attackRange = 1f;
        attackDelay = 1f;
        passedTime = attackDelay;
    }
}
