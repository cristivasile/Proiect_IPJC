using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMagnet : MonoBehaviour
{
    public Transform coinDestroyPoint;
    public float radius = 2f;

    private void Start()
    {
        var collider = gameObject.GetComponent<CircleCollider2D>();
        collider.radius = radius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Coin"))
        {
            collision.gameObject.GetComponent<Coin>().Move(coinDestroyPoint);
        }
    }
}
