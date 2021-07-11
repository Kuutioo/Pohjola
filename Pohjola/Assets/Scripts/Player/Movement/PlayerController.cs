using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Character Attributes:")]
    public float movementSpeed;
    public float rotationSpeed;

    private Animator animator;

    private Vector2 movementDirection;

    private string currentState;

    [HideInInspector]
    public DrinkType drinkType = DrinkType.None;
    private PlayerAnimations playerAnimations = PlayerAnimations.None;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();;
    }

    private void Update()
    {
        Move(); 
    }

    private void Move()
    {
        // Make a Vector2(0, 0)
        movementDirection = Vector2.zero;

        // Apply x and y to the correct axis
        movementDirection.x = Input.GetAxis("Horizontal");
        movementDirection.y = Input.GetAxis("Vertical");

        // inputMagnitude is the length of the movementDirection. Clamp that value between 0 and 1. (Magnitude = length of the vector)
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        // Normalize movementDirection so make its magnitude 1
        movementDirection.Normalize();

        // Move the transform in the direction of the translation. Translation is Space.World so movement is applied relative to the world coordinate system
        transform.Translate(movementDirection * movementSpeed * inputMagnitude * Time.deltaTime, Space.World);

        // movementDirection is not equal to 0, 0
        if (movementDirection != Vector2.zero)
        {
            // Fancy rotation stuff. Don't know how it works
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        }

        Animate();
    }

    private void Animate()
    {
        if (drinkType == DrinkType.None)
        {
            AnimateWalk();
        }
        else
        {
            if (drinkType == DrinkType.Coffee)
            {
                AnimateCoffee();
            }
        }
    }

    private void AnimateWalk()
    {
        if (movementDirection.x != 0 || movementDirection.y != 0)
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

    private void AnimateCoffee()
    {
        if (movementDirection.x != 0 || movementDirection.y != 0)
        {
            if (Input.GetMouseButton(0))
            {
                playerAnimations = PlayerAnimations.Player_Drink_Coffee_Walk_Placeholder;
                ChangeAnimationState(playerAnimations.ToString());
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
            }
            else
            {
                playerAnimations = PlayerAnimations.Player_Idle_Coffee_Placeholder;
                ChangeAnimationState(playerAnimations.ToString());
            }
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
