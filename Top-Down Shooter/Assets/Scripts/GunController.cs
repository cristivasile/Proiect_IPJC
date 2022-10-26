using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private Transform barrel;

    private Vector2 orientation;
    private float angle;

    public GameObject bullet;
    public float bulletSpeed = 10f;
    private float fireTimer = 0f;
    public float fireRate = 2f;


    // Start is called before the first frame update
    void Start()
    {
       barrel  = transform;
    }

    // Update is called once per frame
    void Update()
    {
        orientation = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        angle = -Mathf.Atan2(orientation.x, orientation.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        fireTimer += Time.deltaTime;
        if (fireTimer >= 1 / fireRate && Input.GetMouseButton(0))
        {
            fireTimer = 0;
            Shoot();
        }
    }


    private void Shoot()
    {
        GameObject shot = Instantiate(bullet, barrel.position, barrel.rotation);
        shot.GetComponent<Rigidbody2D>().velocity = barrel.up * bulletSpeed;
    }
}
