using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    // Original source: https://www.youtube.com/channel/UC-BN2PIJpO1p3NNrJHXJPsQ, Pretty Fly Games

    private CarController carController;

    private void Awake()
    {
        carController = GetComponent<CarController>();
    }

    private void Update()
    {
        // Make a Vector2(0, 0)
        Vector2 inputVector = Vector2.zero;

        // Apply x and y to the correct axis
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        // Set the input vectors
        carController.SetInputVector(inputVector);
    }
}
