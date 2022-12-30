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

    public void UpdateText()
    {
        _tmpText.text = $"{(int)_coins.Amount}";
    }
}
