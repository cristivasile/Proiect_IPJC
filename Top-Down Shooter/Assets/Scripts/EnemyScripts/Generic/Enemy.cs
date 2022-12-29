using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    private Health _health;
    private PlayerStats _stats;

    public UnityEvent OnDeath;

    public int enemyValue;
    public GameObject coinPrefab;

    protected float speed = 2f;
    protected float damage = 10f;
    protected float maxHealth = 50f;

    //speed will increase slower than damage and health
    private readonly float speedModifyPercentage = 0.5f;
    private readonly float damageModifyPercentage = 1.0f;
    private readonly float healthModifyPercentage = 1.10f;

    protected float health;

    // Start is called before the first frame update
    protected void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        _health = FindObjectOfType<Health>();
        _stats = FindObjectOfType<PlayerStats>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            OnDeath?.Invoke();
            return;
        }
    }

    public virtual void Destroy()
    {        
        Destroy(gameObject);
    }

    public virtual void DropCoins()
    {
        for (int i = 0; i < enemyValue; i++)
        {
            var randomPositionFactor = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0);
            var coin = Instantiate(coinPrefab, gameObject.transform.position + randomPositionFactor, Quaternion.identity);
            var randomScaleFactor = Random.Range(0.7f, 1f);
            coin.transform.localScale = new Vector3(coin.transform.localScale.x * randomScaleFactor, coin.transform.localScale.y * randomScaleFactor, coin.transform.localScale.z);
        }
    }

    public virtual void ApplyLifeSteal()
    {
        _health.Heal(maxHealth * _stats.LifeSteal.Value / 100);
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

    protected void applyModifier(ref float stat, float modifier, float baseModifier)
    {
        stat += stat * (modifier - 1.0f) * baseModifier; 
    }

    //sets the stats based on a difficulty modifier
    public virtual void ModifyStats(float modifier)
    {
        applyModifier(ref speed, modifier, speedModifyPercentage);
        applyModifier(ref damage, modifier, damageModifyPercentage);
        applyModifier(ref maxHealth, modifier, healthModifyPercentage);
        health = maxHealth;
    }
}
