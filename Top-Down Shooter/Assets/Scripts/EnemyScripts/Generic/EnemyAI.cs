using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyAI : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;
    public UnityEvent OnAttack;

    public Transform player;
    protected float distance;
    protected Vector3 direction;

    protected float chaseRange;
    protected float attackRange;

    protected float attackDelay;
    protected float passedTime;

    protected void Start()
    {
        chaseRange = 30f;
        attackRange = 1f;

        attackDelay = 1f;
        passedTime = attackDelay;
    }


    protected void Update()
    {
        if (player == null)
        {
            OnMovementInput?.Invoke(Vector2.zero);
            return;
        }

        distance = Vector2.Distance(player.position, transform.position);
        direction = player.position - transform.position;
        if (distance <= chaseRange)
        {
            OnPointerInput?.Invoke(player.position);

            if(distance <= attackRange)
            {
                OnMovementInput?.Invoke(Vector2.zero);
                if(passedTime >= attackDelay)
                {
                    passedTime = 0f;
                    OnAttack?.Invoke();
                }
            }
            else
            {
                foreach (var enemyAi in FindObjectsOfType<EnemyAI>())
                {
                    if(Vector3.Distance(enemyAi.transform.position, transform.position) < 1f)
                    {
                        direction -= (enemyAi.transform.position - transform.position).normalized * 0.8f;
                    }
                }
                OnMovementInput?.Invoke(direction.normalized);
            }
        }
        if (distance > chaseRange)
        {
            OnMovementInput?.Invoke(Vector2.zero);
        }

        if (passedTime < attackDelay)
        {
            passedTime += Time.deltaTime;
        }
    }
}
