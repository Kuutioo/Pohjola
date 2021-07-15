using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerCoffee : PlayerState
{
    public PlayerCoffee(PlayerController _playerController) : base(_playerController)
    {
    }

    public override void ChangeState()
    {
        if (playerController.movementDirection.x != 0 || playerController.movementDirection.y != 0)
        {
            if (Input.GetMouseButton(0))
            {
                playerController.playerAnimations = PlayerAnimations.Player_Drink_Coffee_Walk_Placeholder;
                playerController.ChangeAnimationState(playerController.playerAnimations.ToString());
                playerController.currentDrink.ChangeDrinkAmount(0.0001f);
            }

            else
            {
                playerController.playerAnimations = PlayerAnimations.Player_Walk_Coffee_Placeholder;
                playerController.ChangeAnimationState(playerController.playerAnimations.ToString());
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                playerController.playerAnimations = PlayerAnimations.Player_Drink_Coffee_Idle_Placeholder;
                playerController.ChangeAnimationState(playerController.playerAnimations.ToString());
                playerController.currentDrink.ChangeDrinkAmount(0.0001f);

            }
            else
            {
                playerController.playerAnimations = PlayerAnimations.Player_Idle_Coffee_Placeholder;
                playerController.ChangeAnimationState(playerController.playerAnimations.ToString());
            }
        }

        if (playerController.currentDrink.currentDrinkVolume <= 0.0f)
        {
            playerController.currentDrinkType = DrinkType.None;
            playerController.drinkItem.SetActive(false);
            playerController.SetState(new PlayerIdle(playerController));

        }
        return;
    }


}
