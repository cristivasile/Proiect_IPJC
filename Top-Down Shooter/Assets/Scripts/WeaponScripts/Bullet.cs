using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;
    public int pierce = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);

                if (pierce == 0)
                    Destroy(gameObject);
                else
                    pierce--;
            }
        }
        else if(collision.gameObject.CompareTag("Wall"))
            Destroy(gameObject);
    }
}
