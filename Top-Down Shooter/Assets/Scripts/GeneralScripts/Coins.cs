using System;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField]
    private int _amount;

    public int Amount
    {
        get
        {
            return _amount;
        }
        set
        {
            _amount = value;
            OnChange?.Invoke();
        }
    }

    public Action OnChange;


    public void AddAmount(int amount)
    {
        Amount += amount;
    }

    public void SubtractAmount(int amount)
    {
        Amount -= amount;

        if (Amount < 0)
            Amount = 0;
    }
}
