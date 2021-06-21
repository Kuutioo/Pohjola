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

    public GameObject player;

    public bool canEnterCar = false;
    public bool pressed = false;

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
            car.GetComponent<CarController>().enabled = !car.GetComponent<CarController>().enabled;

            leftTrail.GetComponent<WheelTrailRendererHandler>().enabled = !leftTrail.GetComponent<WheelTrailRendererHandler>().enabled;
            rightTrail.GetComponent<WheelTrailRendererHandler>().enabled = !rightTrail.GetComponent<WheelTrailRendererHandler>().enabled;

            leftParticle.GetComponent<WheelParticleHandler>().enabled = !leftParticle.GetComponent<WheelParticleHandler>().enabled;
            rightParticle.GetComponent<WheelParticleHandler>().enabled = !rightParticle.GetComponent<WheelParticleHandler>().enabled;


            player.GetComponent<Rigidbody2D>().isKinematic = !player.GetComponent<Rigidbody2D>().isKinematic;
            player.GetComponent<PlayerController>().enabled = !player.GetComponent<PlayerController>().enabled;
            player.transform.parent = car.transform;
            player.SetActive(false);

            if (pressed && Input.GetKeyDown(KeyCode.F))
            {
                player.transform.parent = null;
                pressed = false;
                player.SetActive(true);
                return;
            }
            pressed = true;
        }
    }
}
