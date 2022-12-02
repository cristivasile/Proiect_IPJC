using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponController : MonoBehaviour
{
    private Transform barrel;

    public GameObject player;
    public GameObject bullet;

    public RangedWeapon weapon;
    private float fireTimer = 0f;


    // Start is called before the first frame update
    void Start()
    {
       barrel  = transform;
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= 1 / weapon.fireRate && Input.GetMouseButton(0))
        {
            fireTimer = 0;
            Shoot();
        }
    }


    private void Shoot()
    {
        GameObject shot = Instantiate(bullet, barrel.position, barrel.rotation);
        shot.GetComponent<BulletController>().shotFrom = player.transform;
        shot.GetComponent<Rigidbody2D>().velocity = barrel.up * weapon.bulletSpeed;
    }
}
