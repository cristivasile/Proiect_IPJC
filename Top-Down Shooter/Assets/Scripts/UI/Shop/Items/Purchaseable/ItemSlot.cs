using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    private GameObject weaponPrefab;

    public Image icon;
    public TMP_Text description;
    public TMP_Text FP;
    public TMP_Text DMG;
    public TMP_Text FR;

    public void CreateItem(GameObject weaponGO)
    {
        weaponPrefab = weaponGO;
        var weapon = weaponGO.GetComponent<Weapon>();

        icon.sprite = weaponGO.GetComponent<SpriteRenderer>().sprite;
        icon.preserveAspect = true;
        description.text = weaponGO.name;

        FP.text = weapon.fireForce.ToString();
        DMG.text = weapon.bulletDamage.ToString();
        FR.text = weapon.fireRate.ToString();
    }

    public void BuyItem(bool validate = true)
    {
        var shop = FindObjectOfType<Shop>();

        if (EquippedItem.totalItems < shop.maxEquippedItems)
        {
            if (validate)
            {
                if (shop.ValidateWeaponPurchase())
                {
                    var weapons = FindObjectOfType<PlayerWeapons>();
                    weapons.AddWeapon(weaponPrefab);

                    var equippedItems = FindObjectOfType<EquippedItems>();
                    equippedItems.AddItemToInventory(weaponPrefab.GetComponent<SpriteRenderer>().sprite);

                    gameObject.SetActive(false);
                    FindObjectOfType<ShopItems>().CreateItemSlot();
                    Destroy(gameObject);
                }
                else
                    Debug.Log("Not enough coins!");
            }
            else
            {
                var weapons = FindObjectOfType<PlayerWeapons>();
                weapons.AddWeapon(weaponPrefab);

                var equippedItems = FindObjectOfType<EquippedItems>();
                equippedItems.AddItemToInventory(weaponPrefab.GetComponent<SpriteRenderer>().sprite);
            }
        }
    }
}
