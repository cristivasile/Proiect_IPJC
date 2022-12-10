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
    public float attackSpeed = 1f;

    [Header("Attack Positions")]
    public Transform attackDown;
    public Transform attackLeft;
    public Transform attackUp;
    public Transform attackRight;

    [Header("Projectile")]
    public GameObject bullet;
    public float bulletSpeed;

    private float distanceToPlayer;
    private AttackDirection attackDirection;
    private float attackTimer;

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
        TickAttackTimer();

        Attack();

        Move();
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

    public void Attack() {
        if (attackTimer <= 0f) {
            Shoot();
            attackTimer = attackSpeed;
        }
    }

    public void Shoot() {
        Rigidbody2D bulletRb = null;
        Vector2 bulletDirection;

        if (attackDirection == AttackDirection.Down)
        {
            bulletRb = (Instantiate(bullet, attackDown.transform.position, Quaternion.identity) as GameObject).GetComponent<Rigidbody2D>();
        }
        else if (attackDirection == AttackDirection.Left)
        {
            bulletRb = (Instantiate(bullet, attackLeft.transform.position, Quaternion.identity) as GameObject).GetComponent<Rigidbody2D>();
        }
        else if (attackDirection == AttackDirection.Up)
        {
            bulletRb = (Instantiate(bullet, attackUp.transform.position, Quaternion.identity) as GameObject).GetComponent<Rigidbody2D>();
        }
        else if (attackDirection == AttackDirection.Right)
        {
            bulletRb = (Instantiate(bullet, attackRight.transform.position, Quaternion.identity) as GameObject).GetComponent<Rigidbody2D>();
        }


        Vector3 targetPosition = new Vector3(player.transform.position.x - bulletRb.transform.position.x, player.transform.position.y - bulletRb.transform.position.y, 0f);


        bulletRb.transform.up = targetPosition;
        bulletDirection =(player.transform.position - transform.position).normalized;
        bulletRb.AddForce(bulletDirection * bulletSpeed);
    }

    private void CalculateDistanceToPlayer() {
        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
        distanceToPlayer = Vector2.Distance(playerPosition, transform.position);
    }

    private void SetAttackDirection() {
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

    private void TickAttackTimer() {
        if (attackTimer > 0f) attackTimer -= Time.deltaTime;
    }

    /// <summary>
    /// Should be called on death.
    /// </summary>
    protected override void Die()
    {
        Destroy(gameObject);
        instances--;

        // add coins to player
        player.GetComponent<PlayerController>().AddCoins(3);

        // determine if the player receives health via life steal
        var randomValue = Random.Range(0f, 100f);

        if (randomValue < player.GetComponent<PlayerController>().lifeSteal)
        {
            player.GetComponent<PlayerController>().Heal(1);
        }
    }

}
