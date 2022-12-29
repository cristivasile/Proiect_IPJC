using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WavesText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _tmpText;

    [SerializeField]
    public WaveNotifier waves;

    public void UpdateText()
    {
        _tmpText.text = waves.CurrentWaveText;
    }
}
