using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float damage = 1f;
    /// <summary>
    /// Number of enemies the bullet can punch through before it is destroyed
    /// </summary>
    public int piercing = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
            Destroy(gameObject);

        if (other.gameObject.CompareTag("Enemy"))
        {
            piercing--;
            if (piercing == 0)
                Destroy(gameObject);

            other.gameObject.GetComponent<BaseEnemy>().TakeDamage(damage);
        }

    }
}
