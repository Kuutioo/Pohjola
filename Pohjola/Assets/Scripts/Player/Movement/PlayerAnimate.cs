using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{
    private string currentState;

    private PlayerController playerController;
    private Drink[] drink;
    private Animator animator;

    private GameObject drinkItem;

    private PlayerAnimations playerAnimations = PlayerAnimations.None;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponentInChildren<Animator>();

        drink = FindObjectsOfType<Drink>();
        drinkItem = GameObject.Find("Drink_Item");
        
    }

    public void AnimateWalk()
    {
        if (playerController.movementDirection.x != 0 || playerController.movementDirection.y != 0)
        {
            playerAnimations = PlayerAnimations.Player_Walk_Placeholder;
            ChangeAnimationState(playerAnimations.ToString());
        }
        else
        {
            playerAnimations = PlayerAnimations.Player_Idle_Placeholder;
            ChangeAnimationState(playerAnimations.ToString());
        }
    }

    public void AnimateCoffee()
    {
        foreach (var item in drink)
        {
            Debug.Log(item);
        }

        if (playerController.movementDirection.x != 0 || playerController.movementDirection.y != 0)
        {
            if (Input.GetMouseButton(0))
            {
                playerAnimations = PlayerAnimations.Player_Drink_Coffee_Walk_Placeholder;
                ChangeAnimationState(playerAnimations.ToString());
                drink[0].ChangeDrinkAmount(0.0001f);
            }

            else
            {
                playerAnimations = PlayerAnimations.Player_Walk_Coffee_Placeholder;
                ChangeAnimationState(playerAnimations.ToString());
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                playerAnimations = PlayerAnimations.Player_Drink_Coffee_Idle_Placeholder;
                ChangeAnimationState(playerAnimations.ToString());
                drink[0].ChangeDrinkAmount(0.0001f);
            }
            else
            {
                playerAnimations = PlayerAnimations.Player_Idle_Coffee_Placeholder;
                ChangeAnimationState(playerAnimations.ToString());
            }
        }

        if (drink[0].currentDrinkVolume <= 0.0f)
        {
            playerController.currentDrinkType = DrinkType.None;
            drinkItem.SetActive(false);
        }
    }

    private void ChangeAnimationState(string newState)
    {
        // Stop the same animation from interrupting itself
        if (currentState == newState) return;

        // Play the animation
        animator.Play(newState);

        // Reassign the current state
        currentState = newState;
    }
}
