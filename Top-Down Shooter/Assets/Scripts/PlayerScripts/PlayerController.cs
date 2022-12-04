using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public PlayerVisuals playerVisuals;
    public HealthBar healthBar;

    // --- Player movement ---
    Vector2 movement;

    //stats for health
    public float maxHealth = 100f;
    public float currentHealth;

    //rest of the stats in percentage
    public float speed = 100f;
    public float lifeSteal = 0f;
    public float damage = 100f;
    public float atackSpeed = 100f;
    public float critChance = 10f;
    public float range = 100f;
    public float armor = 0f;
    public float dodgeChance = 10f;
    public float harvesting = 100f;

    public float coins = 0f;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        playerVisuals.ChangeFacingDirection(movement.x);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = movement.normalized;
        rb.MovePosition(rb.position + direction * speed / 100 * 5f * Time.fixedDeltaTime);
    }

    // Player damage
    public void TakeDamage(float damage)
    {
        System.Random random = new();
        var rng = random.Next(0, 100);

        //determine if the hit connects due to dodge  chance
        if (rng >= Math.Min(dodgeChance, 60))
        {
            //determine the actual damage taken
            if (armor >= 0)
            {
                damage *= 100 / (100 + armor);
            }
            else
            {
                damage *= (2 - 100 / (100 - armor));
            }
            
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }
    
    // Player healing
    public void Heal(float healPoints)
    {
        currentHealth += healPoints;
        currentHealth = Math.Min(maxHealth, currentHealth);
    }

    // Player death
    private void Die()
    {
        Destroy(gameObject);
    }
}