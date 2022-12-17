using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private static int instances = 0;
    protected Rigidbody2D rb;
    private Health _health;

    public int enemyValue;
    public GameObject coinPrefab;

    protected float speed = 2f;
    protected float damage = 10f;
    protected float health = 50f;

    // Start is called before the first frame update
    protected void Start()
    {
        instances++;
        rb = GetComponent<Rigidbody2D>();
        _health = FindObjectOfType<Health>();
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
    protected virtual void Die()
    {
        for(int i = 0; i < enemyValue; i++)
        {
            var randomPositionFactor = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0);
            var coin = Instantiate(coinPrefab, gameObject.transform.position + randomPositionFactor, Quaternion.identity);
            var randomScaleFactor = Random.Range(0.7f, 1f);
            coin.transform.localScale = new Vector3(coin.transform.localScale.x * randomScaleFactor, coin.transform.localScale.y * randomScaleFactor, coin.transform.localScale.z);
        }
        
        Destroy(gameObject);
        instances--;
    }

    public static int GetNumberOfInstances()
    {
        return instances;
    }


    public virtual void Move(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }

    public virtual void PointTo(Vector2 position)
    {
        transform.rotation = Utils.GetRelativeRotation(transform.position, position) * Quaternion.Euler(0, 0, 90);
    }

    public virtual void Attack()
    {
        _health.Damage(damage);
    }
}
