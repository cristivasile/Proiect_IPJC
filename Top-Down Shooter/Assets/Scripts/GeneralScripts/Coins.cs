using System;
using UnityEngine;
using UnityEngine.Events;

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

    public UnityEvent OnChange;


    public void AddAmount(int amount)
    {
        Amount += amount;
    }

    public bool SubtractAmount(int amount)
    {
        if (Amount >= amount)
        {
            Amount -= amount;
            return true;
        }
        return false;
    }
}
