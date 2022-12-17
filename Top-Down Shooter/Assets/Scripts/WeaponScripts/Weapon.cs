using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    // stats
    public float fireForce;
    public float bulletDamage;
    public int bulletPierce;
    public float fireRate;
    private float lastShot;

    Vector3 mousePosition;
    Vector2 aimDirection;

    // --- Player rotation ---
    public float maxTurnSpeed = 720f; // maximum rotation per second (in degrees)
    public float smoothTime = 0.05f; // estimated time for the whole rotation (in seconds)
    private float angle, currentAngularVelocity;

    private void Start()
    {
        // implementing the values foir stats
        fireForce = 20f;
        bulletDamage = 10f;
        bulletPierce = 0;
        fireRate = 5f;
        lastShot = 0f;
    }

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        UpdateWeaponRotation();

        lastShot += Time.deltaTime;
        if(lastShot > 1 / fireRate)
        {
            if (Input.GetButton("Fire1"))
            {
                Shoot();
                lastShot = 0;
            }
        }
    }

    private void UpdateWeaponRotation()
    {
        aimDirection = mousePosition - transform.position;
        float targetAngle = Vector2.SignedAngle(Vector2.up, aimDirection); // returns the angle in degrees (-180, 180)

        angle = Mathf.SmoothDampAngle(angle, targetAngle, ref currentAngularVelocity, smoothTime, maxTurnSpeed); // similar to Quaternion.Lerp()
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 90f));

        if (aimDirection.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (aimDirection.x < 0)
            transform.localScale = new Vector3(1, -1, 1);
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.damage = bulletDamage;
        bulletScript.pierce = bulletPierce;

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(firePoint.right * fireForce, ForceMode2D.Impulse);
    }
}
