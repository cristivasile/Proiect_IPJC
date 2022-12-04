using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    public int maxWeaponCount = 6;
    public float weaponsOffset = 4f;
    public List<GameObject> weapons;
    public Transform pivot;
    public GameObject weaponPrefab;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            AddWeapon(weaponPrefab);
        }
    }

    public void AddWeapon(GameObject newWeapon)
    {
        if (maxWeaponCount - weapons.Count > 0)
        {
            GameObject weaponGo = Instantiate(newWeapon, transform);
            weaponGo.name = "Weapon";
            weapons.Add(weaponGo);

            for (int i = 0; i < weapons.Count; i++)
            {
                var weapon = weapons[i];
                var angle = 360f / weapons.Count;
                weapon.transform.localPosition = Vector3.up * weaponsOffset;
                weapon.transform.RotateAround(pivot.position, Vector3.forward, angle * i);
            }
        }
    }
}
