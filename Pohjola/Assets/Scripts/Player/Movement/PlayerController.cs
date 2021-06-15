using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables for movement direction and speed
    private Vector2 movementDirection;
    private float movementSpeed;

    [Header("Character Attributes:")]
    public float BASE_MOVEMENT_SPEED = 2.0f;

    [Header("References:")]
    public Animator animator;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ProcessInputs();
        Move();
        Animate();
    }
      
    private void ProcessInputs()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();

        //float movementDirection = Input.GetAxis("Vertical");
        rb.AddForce(gameObject.transform.right * BASE_MOVEMENT_SPEED * movementDirection.y);
    }

    private void Move()
    {
        //rb.velocity = movementDirection * movementSpeed * BASE_MOVEMENT_SPEED;
    }

    private void Animate()
    {
        //animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);
        //animator.SetFloat("Speed", movementSpeed);
    }

}
