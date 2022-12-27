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

    public void UpdateBar()
    {
        _slider.value = _health.Ratio;
        _fillImage.color = gradient.Evaluate(_slider.normalizedValue);
    }
}
