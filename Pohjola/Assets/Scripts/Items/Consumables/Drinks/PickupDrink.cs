using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDrink : MonoBehaviour
{
    private const string PLAYER_NAME = "Player";

    private bool inTrigger = false;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && inTrigger)
        {
            gameObject.SetActive(false);
            playerController.drinkType = DrinkType.Coffee;
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
