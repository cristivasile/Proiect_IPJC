using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public int value = 1;
    public float floatSpeed = 10f;

    private Rigidbody2D rb;
    public CoinBurst burst;
    private Transform coinPickup = null;
    private Coins coins;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        coins = FindObjectOfType<Coins>();
    }

    private void FixedUpdate()
    {
        if(coinPickup != null)
            rb.velocity = (coinPickup.position - transform.position).normalized * floatSpeed;
        else
            rb.velocity = Vector3.zero;
    }

    public void Move(Transform transform)
    {
        coinPickup = transform;
    }
    public void Stop()
    {
        coinPickup = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CoinCollect"))
        {
            Stop();
            burst.Play();
        }
    }

    public void DestroyObj(float duration)
    {
        Destroy(gameObject, duration);

        coins.AddAmount(value);
    }
}
