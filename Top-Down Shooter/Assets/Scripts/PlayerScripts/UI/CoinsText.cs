using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _tmpText;
    [SerializeField]
    private Coins _coins;

    private void OnEnable()
    {
        // subscribes to the _coins.OnChange action
        // calls UpdateText() each time OnChange is invoked
        _coins.OnChange += UpdateText;
    }

    private void OnDisable()
    {
        // unsubscribes from the _coins.OnChange action
        _coins.OnChange -= UpdateText;
    }

    public void UpdateText()
    {
        _tmpText.text = $"{(int)_coins.Amount}";
    }
}
