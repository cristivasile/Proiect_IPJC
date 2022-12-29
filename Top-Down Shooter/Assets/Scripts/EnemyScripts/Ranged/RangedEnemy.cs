using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RangedEnemy : Enemy
{
    [SerializeField]
    private RangedEnemyAI _rangedAI;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireForce = 10.0f;

    private readonly float fireForceModifyPercentage = 0.15f;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        speed = 3f;
        damage = 5f;
        health = 20f;
    }

    public override void Attack()
    {
        Shoot();
    }

    private void Shoot()
    {
        Rigidbody2D bulletRb = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation).GetComponent<Rigidbody2D>();
        bulletRb.AddForce(firePoint.right * fireForce, ForceMode2D.Impulse);
    }

    public void Run(Vector2 direction)
    {
        var velocity = direction * speed;
        velocity = Vector3.MoveTowards(velocity, Vector3.zero, 2.0f * Time.deltaTime);
        rb.velocity = velocity;
    }

    public override void ModifyStats(float modifier)
    {
        base.ModifyStats(modifier);
        applyModifier(ref fireForce, modifier, fireForceModifyPercentage);
    }
}
