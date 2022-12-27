using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RangedEnemyAI : EnemyAI
{
    public UnityEvent<Vector2> OnRunInput;
    protected float runRange;

    private new void Start()
    {
        base.Start();

        chaseRange = 16f;
        runRange = 2f;
        attackRange = 5f;
        attackDelay = 1.5f;
        passedTime = attackDelay;
    }

    private new void Update()
    {
        base.Update();
        if(distance < runRange)
        {
            OnRunInput?.Invoke((-direction).normalized);
        }
    }


}
