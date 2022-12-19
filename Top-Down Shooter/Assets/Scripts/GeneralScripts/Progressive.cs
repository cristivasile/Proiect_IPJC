using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Progressive : MonoBehaviour
{
    [SerializeField]
    private float _initial;
    [SerializeField]
    private float _current;

    

    public float Current
    {
        get
        {
            return _current;
        }
        set
        {
            _current = value;
            OnChange?.Invoke();
        }
    }

    public float Initial
    {
        get
        {
            return _initial;
        }
    }

    public float Ratio => _current / _initial;

    public UnityEvent OnChange;

    private void Awake()
    {
        Current = Initial;
    }
}
