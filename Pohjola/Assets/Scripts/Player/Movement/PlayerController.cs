using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Setting animations with strings
    public static readonly int walkingAnimID = Animator.StringToHash("IsWalking");

    [Header("Character Attributes:")]
    public float movementSpeed;
    public float rotationSpeed;

    private Animator animator;

    private Vector2 movementDirection;

    private DrinkType drinkType;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
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

        AnimateWalk();
    }

    private void AnimateWalk()
    {
        // Animation stuff. Simple
        if (movementDirection.x != 0 || movementDirection.y != 0)
        {
            animator.SetBool(walkingAnimID, true);
        }
        else
        {
            animator.SetBool(walkingAnimID, false);
        }
    }
}
