using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float health = 50.0f;

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

        rb.velocity = new Vector3(horizontalInput * speed, verticalInput * speed, 0);
    }

    public void TakeDamage(float damage) {
        health = health - damage;

        if (health <= 0f) {
            Die();
        }
    }

    private void Die() {
        Destroy(gameObject);
    }

}
