using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;
    public int piercing = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            Destroy(gameObject);

        BaseEnemy enemy = collision.GetComponent<BaseEnemy>();
        if (enemy != null)
        {
            if (piercing == 0)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
            else
                piercing--;
        }
    }
}
