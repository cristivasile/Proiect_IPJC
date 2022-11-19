using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{

    public static GameObject player;
    protected float speed = 2f;
    protected float damage = 1f;
    protected float health = 10f;

    protected Rigidbody2D rb;

    private static int instances = 0;

    const float knockbackTime = 0.1f;
    public float knockbackTimer = 0f;


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
        if (knockbackTimer >= 0) {
            knockbackTimer -= Time.deltaTime;
        }
    }


    /// <summary>
    /// Should be called when the enemy is hit.
    /// </summary>
    /// <param name="senderTransform"> - position of the hitter </param>
    /// <param name="knockbackStrength"> - strength of the knockback dealt </param>
    /// <param name="hitValue"> - damage dealt by the hit</param>
    public void TakeHit(Transform senderTransform, float knockbackStrength, float hitValue)
    {
        health -= hitValue;

        if (health <= 0)
        {
            Die();
            return;
        }

        TakeKnockback(senderTransform, knockbackStrength);
    }


    private void TakeKnockback(Transform senderTransform, float strength)
    {
        Vector2 direction = (transform.position - senderTransform.position).normalized;
        knockbackTimer = knockbackTime;
        rb.velocity = Vector2.zero;
        rb.AddForce(direction.normalized * strength, ForceMode2D.Impulse);

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
