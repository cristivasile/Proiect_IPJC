using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    public PlayerVisuals visuals;
    public PlayerStats stats;

    public Health health;
    public Coins coins;

    Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        visuals.ChangeFacingDirection(movement.x);
    }

    private void FixedUpdate()
    {
        Vector2 direction = movement.normalized;
        rb.MovePosition(rb.position + direction * stats.speed / 100 * stats.unitSpeed * Time.fixedDeltaTime);
    }

    // Player damage
    public void ApplyDamage(float damageAmount)
    {
        var randomValue = Random.Range(0f, 100f);

        //determine if the hit connects due to dodge chance
        if (randomValue >= Mathf.Min(stats.dodgeChance, stats.maxDodgeChance))
        {
            //determine the actual damage taken
            if (stats.armor >= 0)
            {
                damageAmount *= 100 / (100 + stats.armor);
            }
            else
            {
                damageAmount *= 2 - 100 / (100 - stats.armor);
            }

            health.Damage(damageAmount);
        }
    }

    // Player healing
    public void ApplyHeal(float healAmount)
    {
        health.Heal(healAmount);
    }

    // Player death
    // TODO change death event
    public void Die()
    {
        // Destroy(gameObject);
        Debug.Log("Player died!");
    }
}