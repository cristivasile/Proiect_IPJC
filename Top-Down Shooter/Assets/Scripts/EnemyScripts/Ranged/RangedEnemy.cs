using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RangedEnemy : Enemy
{
    [SerializeField]
    private RangedEnemyAI _rangedAI;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireForce = 10;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        speed = 3f;
        damage = 5;
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
}
