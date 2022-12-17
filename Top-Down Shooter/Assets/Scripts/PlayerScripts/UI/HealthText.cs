using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _tmpText;
    [SerializeField]
    private Progressive _health;

    private void OnEnable()
    {
        // subscribes to the _health.OnChange action
        // calls UpdateBar() each time OnChange is invoked
        _health.OnChange += UpdateText;
    }

    private void OnDisable()
    {
        // unsubscribes from the _health.OnChange action
        _health.OnChange -= UpdateText;
    }

    public void UpdateText()
    {
        _tmpText.text = $"{(int)_health.Current} / 100";
    }
}