using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{
    private string currentState;

    private PlayerController playerController;
    private Animator animator;

    public Drink drink;

    private PlayerAnimations playerAnimations = PlayerAnimations.None;

    private void Awake()
    {
        playerController = GetComponentInChildren<PlayerController>();
        animator = GetComponentInChildren<Animator>();
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
        if (playerController.movementDirection.x != 0 || playerController.movementDirection.y != 0)
        {
            if (Input.GetMouseButton(0))
            {
                playerAnimations = PlayerAnimations.Player_Drink_Coffee_Walk_Placeholder;
                ChangeAnimationState(playerAnimations.ToString());
                drink.ChangeDrinkAmount(0.0001f);
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
                drink.ChangeDrinkAmount(0.0001f);
            }
            else
            {
                playerAnimations = PlayerAnimations.Player_Idle_Coffee_Placeholder;
                ChangeAnimationState(playerAnimations.ToString());
            }
        }

        if (drink.currentDrinkVolume <= 0.0f)
        {
            playerController.currentDrinkType = DrinkType.None;
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
