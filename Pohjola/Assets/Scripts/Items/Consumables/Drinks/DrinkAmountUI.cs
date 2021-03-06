using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkAmountUI : MonoBehaviour
{
    public Slider slider;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void SetMaxDrinkAmount(float amount)
    {
        slider.maxValue = amount;
        slider.value = amount;
    }

    public void SetDrinkAmount(float amount)
    {
        slider.value = amount;
    }
}
