using System;
using UnityEngine;

public class PlayerController : PlayerStateMachine
{
    // Movement Attributes
    [Header("Player Movement Attributes:")]
    public float movementSpeed;
    public float rotationSpeed;

    // Movement
    internal Vector2 movementDirection;

    // Drinks
    public DrinkType currentDrinkType = DrinkType.None;
    public Drink currentDrink;
    internal GameObject drinkItem;

    // State
    private string currentState;

    // Animator
    private Animator animator;
    internal PlayerAnimations playerAnimations = PlayerAnimations.None;

    // Events
    public event EventHandler OnDrinkEmpty;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        drinkItem = GameObject.Find("Drink_Item");

        SetState(new PlayerIdle(this));
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
            // Fancy rotation stuff. Don't know how it works but it does. Lets just leave it there :)
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        SetCurrentState();
        CheckCurrentDrinkVolume();

        playerState.ChangeState();
    }

    private void CheckCurrentDrinkVolume()
    {
        if (currentDrink != null)
        {
            if (currentDrink.currentDrinkVolume <= 0.0f)
            {
                OnDrinkEmpty?.Invoke(this, EventArgs.Empty);
                currentDrink = null;
            }
        }
    }

    private void SetCurrentState()
    {
        if (currentDrinkType == DrinkType.None)
        {
            SetState(new PlayerIdle(this));
        }
        else
        {
            if (currentDrinkType == DrinkType.Coffee)
            {
                SetState(new PlayerCoffee(this));
            }
        }
    }

    public void ChangeAnimationState(string newState)
    {
        // Stop the same animation from interrupting itself
        if (currentState == newState) return;

        // Play the animation
        animator.Play(newState);

        // Reassign the current state
        currentState = newState;
    }
}
