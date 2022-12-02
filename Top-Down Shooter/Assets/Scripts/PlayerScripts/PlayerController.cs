using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8.0f;
    public float health = 100.0f;
    public Weapon weapon;
    Rigidbody2D rb;
    
    // --- Player movement ---
    Vector2 moveDirection, mousePosition;

    // --- Player rotation ---
    public float maxTurnSpeed = 720f; // maximum rotation per second (in degrees)
    public float smoothTime = 0.1f; // estimated time for the whole rotation (in seconds)
    float angle, currentAngularVelocity;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // mouse position relative to the player
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }
    }

    private void FixedUpdate()
    {
        UpdatePlayerMovement();
        UpdatePlayerRotation();
    }


    private void UpdatePlayerMovement()
    {
        var moveX = Input.GetAxisRaw("Horizontal");
        var moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        rb.velocity = moveDirection * moveSpeed;
    }

    private void UpdatePlayerRotation()
    {
        Vector2 aimDirection = mousePosition - rb.position;
        float targetAngle = Vector2.SignedAngle(Vector2.up, aimDirection); // returns the angle in degrees (-180, 180)
        
        angle = Mathf.SmoothDampAngle(angle, targetAngle, ref currentAngularVelocity, smoothTime, maxTurnSpeed); // similar to Quaternion.Lerp()
        rb.rotation = angle;
    }

    // Player damage
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
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
