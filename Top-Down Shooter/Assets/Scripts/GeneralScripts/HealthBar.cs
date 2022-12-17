using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Progressive _health;
    [SerializeField]
    private Image _fillImage;
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    public Gradient gradient;

    private void OnEnable()
    {
        // subscribes to the _health.OnChange action
        // calls UpdateBar() each time OnChange is invoked
        _health.OnChange += UpdateBar;
    }

    private void OnDisable ()
    {
        // unsubscribes from the _health.OnChange action
        _health.OnChange -= UpdateBar;
    }


    private void UpdateBar()
    {
        _slider.value = _health.Ratio;
        _fillImage.color = gradient.Evaluate(_slider.normalizedValue);
    }
}
