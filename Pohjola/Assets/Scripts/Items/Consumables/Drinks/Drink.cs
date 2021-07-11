using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour
{
    public float drinkVolume = 1.0f;
    public float currentDrinkVolume;

    public DrinkType drinkType = DrinkType.None;

    public DrinkAmountUI drinkAmountUI;

    private void Awake()
    {
        currentDrinkVolume = drinkVolume;

        drinkAmountUI.SetMaxDrinkAmount(drinkVolume);
    }

    public void ChangeDrinkAmount(float amount)
    {
        currentDrinkVolume -= amount;

        drinkAmountUI.SetDrinkAmount(currentDrinkVolume);
    }
}
