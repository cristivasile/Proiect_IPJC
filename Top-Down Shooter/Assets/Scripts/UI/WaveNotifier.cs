using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveNotifier : MonoBehaviour
{
    [SerializeField]
    private string _currentWaveText;

    public string CurrentWaveText
    {
        get
        {
            return _currentWaveText;
        }
        set
        {
            _currentWaveText = value;
            OnChange?.Invoke();
        }
    }

    public void SetWave(int currentWave, int maxWave)
    {
        CurrentWaveText = $"{currentWave} / {maxWave}";
    }

    public UnityEvent OnChange;
}
