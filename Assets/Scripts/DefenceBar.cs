using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class DefenceBar : MonoBehaviour
{
    private Slider _DefenceSlider;
    void Start()
    {
        _DefenceSlider = GetComponent<Slider>();
    }
    public void SetMaxDefence(int defence)
    {
        _DefenceSlider.maxValue = defence;
        _DefenceSlider.value = defence;
    }
    public void SetDefence(int defence)
    {
        _DefenceSlider.value = defence;
    }
}
