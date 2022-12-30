using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void Enable()
    {
        gameObject.GetComponent<Canvas>().enabled = true;
    }
}
