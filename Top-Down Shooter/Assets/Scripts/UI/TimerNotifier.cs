using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerNotifier : MonoBehaviour
{
    public TMP_Text timer;

    public void UpdateText(float remainingTime)
    {
        timer.text = $"{remainingTime:0.00}s";
    }

}
