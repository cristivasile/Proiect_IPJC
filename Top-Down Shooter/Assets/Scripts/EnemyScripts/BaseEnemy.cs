using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{

    public static GameObject player;
    protected float speed = 2f;
    protected float damage = 10f;
    protected float health = 100f;

    protected Rigidbody2D rb;

    private static int instances = 0;


    protected BaseEnemy()
    {
    }

    // Start is called before the first frame update
    protected void Start()
    {
        instances++;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected void Update()
    {
        //face player
        transform.rotation = Utils.GetRelativeRotation(transform.position, player.transform.position);
    }


    /// <summary>
    /// Should be called when the enemy is hit.
    /// </summary>
    /// <param name="damage"> - damage dealt by the hit</param>
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
            return;
        }
    }

    /// <summary>
    /// Should be called on death.
    /// </summary>
    protected void Die()
    {
        Destroy(gameObject);
        instances--;
    }

    public static int GetNumberOfInstances()
    {
        return instances;
    }
}
