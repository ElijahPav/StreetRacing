using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarsController : MonoBehaviour
{
    [SerializeField] Slider _healthSlider;
    [SerializeField] Slider _magnetSlider;
    [SerializeField] Slider _shieldSlider;
    [SerializeField] Slider _nitroSlider;

    private void Start()
    {
        _magnetSlider.gameObject.SetActive(false);
        _shieldSlider.gameObject.SetActive(false);
        _nitroSlider.gameObject.SetActive(false);
    }
    public void UpdateBuffBar(BoosterTypes type, float value)
    {
        switch (type)
        {
            case BoosterTypes.Magnet:
                _magnetSlider.value = value;
                break;
            case BoosterTypes.Shield:
                _shieldSlider.value = value;
                break;
            case BoosterTypes.Nitro:
                _nitroSlider.value = value;
                break;
        }
    }

    public void SetBuffBarActiveState(BoosterTypes type,bool state)
    {
        switch (type)
        {
            case BoosterTypes.Magnet:
                _magnetSlider.gameObject.SetActive(state);
                break;
            case BoosterTypes.Shield:
                _shieldSlider.gameObject.SetActive(state);
                break;
            case BoosterTypes.Nitro:
                _nitroSlider.gameObject.SetActive(state);
                break;
        }
    }
    public void UpdateHealthBar(float value )
    {
        _healthSlider.value = value;
    }
}
