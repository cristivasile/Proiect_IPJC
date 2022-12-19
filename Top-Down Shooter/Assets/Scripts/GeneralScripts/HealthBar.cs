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

    private void Start()
    {
        _slider.minValue = 0f;
        _slider.maxValue = _health.Initial;
    }

    public void UpdateBar()
    {
        _slider.value = _health.Current;
        _fillImage.color = gradient.Evaluate(_slider.normalizedValue);
    }
}
