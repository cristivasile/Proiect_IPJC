using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    public PlayerVisuals playerVisuals;
    public Health health;
    public Coins coins;

    Vector2 movement;

    // default stats
    private readonly float unitSpeed = 5f;
    private readonly float maxDodgeChance = 60f;

    // rest of the stats in percentages
    public float speed = 100f;
    public float lifeSteal = 0f;
    public float damage = 100f;
    public float atackSpeed = 100f;
    public float critChance = 10f;
    public float range = 100f;
    public float armor = 0f;
    public float dodgeChance = 10f;
    public float harvesting = 100f; // multiplier for coins


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        playerVisuals.ChangeFacingDirection(movement.x);
    }

    private void FixedUpdate()
    {
        Vector2 direction = movement.normalized;
        rb.MovePosition(rb.position + direction * speed / 100 * unitSpeed * Time.fixedDeltaTime);
    }

    // Player damage
    public void ApplyDamage(float damageAmount)
    {
        var randomValue = Random.Range(0f, 100f);

        //determine if the hit connects due to dodge chance
        if (randomValue >= Mathf.Min(dodgeChance, maxDodgeChance))
        {
            //determine the actual damage taken
            if (armor >= 0)
            {
                damageAmount *= 100 / (100 + armor);
            }
            else
            {
                damageAmount *= 2 - 100 / (100 - armor);
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