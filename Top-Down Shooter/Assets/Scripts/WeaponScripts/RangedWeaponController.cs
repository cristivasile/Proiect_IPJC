using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponController : MonoBehaviour
{
    private Transform barrel;

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
        transform.rotation = Utils.GetRelativeRotation(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));

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
