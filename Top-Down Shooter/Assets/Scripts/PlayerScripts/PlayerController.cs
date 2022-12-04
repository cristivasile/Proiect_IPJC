using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8.0f;
    public float currentHealth, maxHealth = 100.0f;
    Rigidbody2D rb;
    public PlayerVisuals playerVisuals;
    public HealthBar healthBar;

    // --- Player movement ---
    Vector2 movement;


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
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }

    // Player damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    // Player death
    private void Die()
    {
        Destroy(gameObject);
    }
}
