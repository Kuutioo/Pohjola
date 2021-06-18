using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCar : MonoBehaviour
{
    public GameObject car;

    public GameObject leftTrail;
    public GameObject rightTrail;
    public GameObject leftParticle;
    public GameObject rightParticle;

    private Rigidbody2D rb;

    public bool canEnterCar = false;

    private void Awake()
    {
        car.GetComponent<CarController>().enabled = false;

        leftTrail.GetComponent<WheelTrailRendererHandler>().enabled = false;
        rightTrail.GetComponent<WheelTrailRendererHandler>().enabled = false;

        leftParticle.GetComponent<WheelParticleHandler>().enabled = false;
        rightParticle.GetComponent<WheelParticleHandler>().enabled = false;
    }

    private void Update()
    {
        if (canEnterCar && Input.GetKeyDown(KeyCode.F))
        {
            car.GetComponent<CarController>().enabled = true;

            rb = car.AddComponent<Rigidbody2D>();
            rb.drag = 0;
            rb.angularDrag = 0.1f;
            rb.gravityScale = 0;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        }
    }
}
