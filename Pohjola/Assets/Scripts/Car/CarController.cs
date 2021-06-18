using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Original source: https://www.youtube.com/channel/UC-BN2PIJpO1p3NNrJHXJPsQ, Pretty Fly Games

    /* A lot of Dot product is used in this. Dot product confusing as hell.
       Check these for additional info: https://en.wikipedia.org/wiki/Dot_product, https://docs.unity3d.com/ScriptReference/Vector2.Dot.html
    */

    [Header("Car Attributes:")]
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20.0f;

    private float accelerationInput = 0.0f;
    private float steeringInput = 0.0f;
    private float rotationAngle = 0.0f;

    private float velocityVsUp = 0.0f;

    private Rigidbody2D rb;
    private GameObject gameManager;
    private DisableCar disableCarScript;

    private bool enterCar;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("Game Manager");
        disableCarScript = gameManager.GetComponent<DisableCar>();
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();
        ApplySteering();
        KillOrthogonalVelocity();
    }

    private void ApplyEngineForce()
    {
        // Calculate how much "forward" we are going in terms of the direction of our velocity. No idea what this does
        velocityVsUp = Vector2.Dot(transform.up, rb.velocity);
        Debug.Log(velocityVsUp);

        // Limit so we cannot go faster than the max speed in the "forward" direction
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;

        // Limit so we cannot go faster than the 50% of max speed in "reverse" direction
        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
            return;

        // Limit so we cannnot go faster in any direction while acclerating. Basically sqrMagnitude returns the squared length of the desired vector. See: https://docs.unity3d.com/ScriptReference/Vector2-sqrMagnitude.html
        if (rb.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
            return;

        // Apply drag if there is no accelerationInput so the car stops when the player doesn't press the acceleration button
        if (accelerationInput == 0)
        {
            // Mathf.Lerp returns the interpolated float result between the two float values. Interpolate is a little too fancy word for me See: https://docs.unity3d.com/ScriptReference/Mathf.Lerp.html
            rb.drag = Mathf.Lerp(rb.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else rb.drag = 0;

        // Force for the engine
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        // Apply that engine force and move the car forwards
        rb.AddForce(engineForceVector, ForceMode2D.Force);
    }

    private void ApplySteering()
    {
        // Limit the cars ability to turn when in low speeds
        float minSpeedBeforeAllowTurningFactor = (rb.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        // Update rotation angle
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;

        // Apply steering by rotating car object
        rb.MoveRotation(rotationAngle);
    }

    private void KillOrthogonalVelocity()
    {
        // Calculate forward and right velocity
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);

        // Add the two velocites together and then multiply it with the driftFactor in order to decrease the drift of the car and make it less floaty
        rb.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    float GetLateralVelocity()
    {
        // Dot product = monke
        return Vector2.Dot(transform.right, rb.velocity);
    }

    public bool IsTiresScreeching(out float lateralVelocity, out bool isBraking)
    {
        lateralVelocity = GetLateralVelocity();
        isBraking = false;

        // Check if the car is moving and the player is pressing brake. If so, then tires should screech
        if (accelerationInput < 0 && velocityVsUp > 0)
        {
            isBraking = true;
            return true;
        }

        // If the car has a lot of side movement then the tires should also screech.
        // Check Mathf.Abs: https://docs.unity3d.com/ScriptReference/Mathf.Abs.html
        if (Mathf.Abs(GetLateralVelocity()) > 4.0f)
            return true;

        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Monke");
            Destroy(rb);
            disableCarScript.canEnterCar = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("No more monke");
            disableCarScript.canEnterCar = false;
            this.enabled = false;
        }
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }
}
