using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedItem : MonoBehaviour
{
    public Image icon;
    public static int totalItems = 0;

    public void CreateItem(Sprite sprite)
    {
        totalItems++;
        icon.sprite = sprite;
        icon.preserveAspect = true;
    }
}
