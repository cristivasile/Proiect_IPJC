using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public PlayerVisuals playerVisuals;
    public HealthBar healthBar;

    // --- Player movement ---
    Vector2 movement;

    // stats for health
    public float maxHealth;
    public float currentHealth;

    // stats for coins
    public int coins;

    // default stats
    private readonly float unitSpeed = 8f;
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

        //health stats assigned
        maxHealth = 100f;
        currentHealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);

        // stats for coins
        coins = 0;

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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = movement.normalized;
        rb.MovePosition(rb.position + direction * speed / 100 * unitSpeed * Time.fixedDeltaTime);
    }

    // Player damage
    public void TakeDamage(float damage)
    {
        var randomValue = Random.Range(0f, 100f);

        //determine if the hit connects due to dodge chance
        if (randomValue >= Mathf.Min(dodgeChance, maxDodgeChance))
        {
            //determine the actual damage taken
            if (armor >= 0)
            {
                damage *= 100 / (100 + armor);
            }
            else
            {
                damage *= 2 - 100 / (100 - armor);
            }

            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            if (currentHealth <= 0f)
            {
                Die();
            }
        }
    }

    // Player healing
    public void Heal(float healPoints)
    {
        currentHealth += healPoints;
        currentHealth = Mathf.Min(maxHealth, currentHealth);
    }

    // Player getting coins on enemy kills
    // If the rng < harvesting you receive double coins
    public void AddCoins(int additionalCoins)
    {
        var randomValue = Random.Range(0f, 100f);

        if(randomValue < harvesting)
        {
            additionalCoins *= 2;
        }

        coins += additionalCoins;
    }

    // Player death
    // TODO change death event
    private void Die()
    {
        // Destroy(gameObject);
    }
}