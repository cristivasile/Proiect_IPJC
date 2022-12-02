using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AttackDirection
{
  Down,
  Left,
  Up,
  Right
}

public class RangedEnemy : BaseEnemy
{

    public float attackRange = 5f;
    public float runRange = 2.5f;
    public float runSpeed = 3f;
    public float followSpeed = 4f;

    [Header("Attack Positions")]
    public Transform attackDown;
    public Transform attackLeft;
    public Transform attackUp;
    public Transform attackRight;

    private float distanceToPlayer;
    private AttackDirection attackDirection;
    public RangedEnemy() : base() {

    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        SetAttackDirection();
        CalculateDistanceToPlayer();

        if (knockbackTimer <= 0f) {
            Move();
        }
    }

    public void Move() {

        if (distanceToPlayer <= runRange) {
            Run();
        }
        else if (distanceToPlayer <= attackRange) {
            return;
        }
        else {
            Follow();
        }

    }

    public void Run() {
        Vector2 moveDirection;
        moveDirection = -(player.transform.position - transform.position);
        moveDirection.Normalize();

        rb.velocity = moveDirection * runSpeed;
    }

    public void Follow() {
        Vector2 moveDirection;
        moveDirection = player.transform.position - transform.position;
        moveDirection.Normalize();

        rb.velocity = moveDirection * followSpeed;
    }

    public void CalculateDistanceToPlayer() {
        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
        distanceToPlayer = Vector2.Distance(playerPosition, transform.position);
    }

    public void SetAttackDirection() {
        float minDistance;
        int minDistanceIndex;
        
        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);


        float[] distances = {
                    Vector2.Distance(playerPosition, attackDown.position),
                    Vector2.Distance(playerPosition, attackLeft.position),
                    Vector2.Distance(playerPosition, attackUp.position),
                    Vector2.Distance(playerPosition, attackRight.position)
        };


        minDistance = distances[0];
        minDistanceIndex = 0;

        for (int i = 1; i < 4; ++i) {
            if (minDistance > distances[i])
            {

                minDistance = distances[i];
                minDistanceIndex = i;
            }
        }

        switch (minDistanceIndex)
        {
            case 0:
                attackDirection = AttackDirection.Down;
            break;
            case 1:
                attackDirection = AttackDirection.Left;
            break;
            case 2:
                attackDirection = AttackDirection.Up;
            break;
            case 3:
                attackDirection = AttackDirection.Right;
            break;

        }
    }


}
