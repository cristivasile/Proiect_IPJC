using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //stats for health
    public float maxHealth = 50f;
    public float currentHealth = 50f;

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

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        this.transform.rotation = Utils.GetRelativeRotation(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        MovePlayer();

    }

    private void MovePlayer()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector3(horizontalInput * 5 * speed / 100, verticalInput * 5 * speed / 100, 0);
    }

    public void Heal(float healPoints)
    {
        currentHealth += healPoints;
        currentHealth = Math.Min(maxHealth, currentHealth);
    }

    public void TakeDamage(float damage) {
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

            if (currentHealth <= 0f)
            {
                Die();
            }
        }
    }

    private void Die() {
        Destroy(gameObject);
    }

}
