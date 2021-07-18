using UnityEngine;

public class PlayerController : PlayerStateMachine
{
    [Header("Player Movement Attributes:")]
    public float movementSpeed;
    public float rotationSpeed;

    internal Vector2 movementDirection;

    public DrinkType currentDrinkType = DrinkType.None;
    public Drink currentDrink;

    private string currentState;

    internal Drink[] drink;
    private Animator animator;

    internal GameObject drinkItem;

    internal PlayerAnimations playerAnimations = PlayerAnimations.None;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        drink = FindObjectsOfType<Drink>();
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

        playerState.ChangeState();
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
