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
    public float speed;
    public float lifeSteal;
    public float damage;
    public float atackSpeed;
    public float critChance;
    public float armor;
    public float dodgeChance;
    public float harvesting; // multiplier for coins


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // rest of the stats in percentages
        speed = 100f;
        lifeSteal = 0f;
        damage = 100f;
        atackSpeed = 100f;
        critChance = 10f;
        armor = 0f;
        dodgeChance = 10f;
        harvesting = 0f;
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