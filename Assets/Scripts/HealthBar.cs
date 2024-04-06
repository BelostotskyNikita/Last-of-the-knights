using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    private Slider _HealthSlider;
    void Start()
    {
        _HealthSlider = GetComponent<Slider>();
    }
    public void SetMaxHealth(int health)
    {
        _HealthSlider.maxValue = health;
        _HealthSlider.value = health;
    }
    public void SetHealth(int health)
    {
        _HealthSlider.value = health;
    }
}
