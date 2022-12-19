using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _tmpText;
    [SerializeField]
    private Progressive _health;

    public void UpdateText()
    {
        _tmpText.text = $"{(int)_health.Current} / {(int)_health.Initial}";
    }
}