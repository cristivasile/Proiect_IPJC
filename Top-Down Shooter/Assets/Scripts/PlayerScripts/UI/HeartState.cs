using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HeartState : MonoBehaviour
{
    public Health health;
    public Sprite[] spriteArray;
    public Image image;

    public void UpdateSprite()
    {
        var numberOfSprites = spriteArray.Count() - 1;
        var lostHealthPercentage = 1f - health.Ratio;
        var spriteIndex = Mathf.FloorToInt(lostHealthPercentage * numberOfSprites + 0.000001f);
        image.sprite = spriteArray[spriteIndex];
    }
}
