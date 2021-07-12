using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDrink : MonoBehaviour
{
    private const string PLAYER_NAME = "Player";

    private bool inTrigger = false;

    private PlayerController playerController;
    private Drink drink;

    private GameObject drinkItem;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        drink = GetComponent<Drink>();
        drinkItem = GameObject.Find("Drink_Item");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inTrigger)
        {
            drinkItem.gameObject.SetActive(true);
            gameObject.SetActive(false);
            CheckDrink(drink.drinkType);
        }
    }

    private void CheckDrink(DrinkType dt)
    {
        if (dt == drink.drinkType)
        {
            playerController.currentDrinkType = dt;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_NAME))
        {
            inTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_NAME))
        {
            inTrigger = false;
        }
    }
}
