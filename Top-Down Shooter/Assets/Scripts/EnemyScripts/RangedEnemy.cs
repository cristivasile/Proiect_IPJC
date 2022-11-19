using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : BaseEnemy
{

    public float attackRange = 5f;
    public float runRange = 2.5f;
    public float runSpeed = 3f;
    public float followSpeed = 4f;


    private float distanceToPlayer;

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

        if (knockbackTimer <= 0f) {
            Move();
        }
    }

    public void CalculateDistanceToPlayer() {
        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
        distanceToPlayer = Vector2.Distance(playerPosition, transform.position);
    }

    public void Move() {
        CalculateDistanceToPlayer();

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
}
