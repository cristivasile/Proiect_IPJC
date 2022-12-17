using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField]
    private MeleeEnemyAI _meleeAI;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }
}
