using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{

    public MeleeEnemy() : base() { }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

        // Update is called once per frame
    new void Update()
    {
        base.Update();

        Follow();
    }

    public void Follow() {
        Vector2 moveDirection;
        moveDirection = player.transform.position - transform.position;
        moveDirection.Normalize();

        rb.velocity = moveDirection * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponentInParent<PlayerController>().TakeDamage(damage);
        }
    }
}
