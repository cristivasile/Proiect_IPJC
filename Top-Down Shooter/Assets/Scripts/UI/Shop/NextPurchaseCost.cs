using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NextPurchaseCost : MonoBehaviour
{
    public TMP_Text upgradeCostText;
    public TMP_Text weaponCostText;

    public void UpdateUpgradeText(int cost)
    {
        upgradeCostText.text = cost.ToString();
    }

    public void UpdateWeaponText(int cost)
    {
        weaponCostText.text = cost.ToString();
    }
}
