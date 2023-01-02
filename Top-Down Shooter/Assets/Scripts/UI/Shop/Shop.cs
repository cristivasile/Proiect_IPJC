using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Coins coins;
    private NextPurchaseCost nextPurchase;
    public bool isEnabled = false;

    public int maxEquippedItems = 6;

    public int currentUpgradePrice = 50;
    public int upgradePercentageIncrease = 20;

    public int currentWeaponPrice = 0;
    public int weaponPercentageIncrease = 100;


    public bool ValidateUpgradePurchase()
    {
        if(coins.SubtractAmount(currentUpgradePrice))
        {
            UpdateUpgradePrice();
            return true;
        }
        return false;
    }

    public bool ValidateWeaponPurchase()
    {
        if (coins.SubtractAmount(currentWeaponPrice))
        {
            UpdateWeaponPrice();
            return true;
        }
        return false;
    }

    public void UpdateUpgradePrice()
    {
        var priceIncreaseFactor = 1f + upgradePercentageIncrease / 100f;
        currentUpgradePrice = Mathf.FloorToInt(priceIncreaseFactor * currentUpgradePrice);
        nextPurchase.UpdateUpgradeText(currentUpgradePrice);
    }

    public void UpdateWeaponPrice()
    {
        var priceIncreaseFactor = 1f + weaponPercentageIncrease / 100f;
        currentWeaponPrice = Mathf.FloorToInt(priceIncreaseFactor * currentWeaponPrice);
        nextPurchase.UpdateWeaponText(currentWeaponPrice);
    }

    private void Start()
    {
        gameObject.GetComponent<Canvas>().enabled = isEnabled;

        nextPurchase = gameObject.GetComponent<NextPurchaseCost>();
        nextPurchase.UpdateUpgradeText(currentUpgradePrice);
        nextPurchase.UpdateWeaponText(currentWeaponPrice);
    }

    public void EnableShop(bool enable)
    {
        isEnabled = enable;
        gameObject.GetComponent<Canvas>().enabled = isEnabled;
        Time.timeScale = isEnabled ? 0f : 1f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isEnabled)
        {
            EnableShop(false);
        }
    }
}
