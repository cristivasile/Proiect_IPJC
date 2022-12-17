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

    private void OnEnable()
    {
        // subscribes to the health.OnChange action
        // calls UpdateBar() each time OnChange is invoked
        health.OnChange += UpdateSprite;
    }

    private void OnDisable()
    {
        // unsubscribes from the health.OnChange action
        health.OnChange -= UpdateSprite;
    }

    private void UpdateSprite()
    {
        var numberOfSprites = (spriteArray.Count() - 1);
        var spriteIndex = Mathf.FloorToInt((1f - health.Ratio) * numberOfSprites + 0.00001f);
        image.sprite = spriteArray[spriteIndex];
    }
}
