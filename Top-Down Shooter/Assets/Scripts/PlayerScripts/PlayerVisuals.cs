using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    public bool facingRight = true;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeFacingDirection(float movementX)
    {
        if (movementX > 0)
        {
            facingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movementX < 0)
        {
            facingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public Color GetPlayerColor()
    {
        return spriteRenderer.color;
    }
}
