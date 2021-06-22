using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DisableCar : MonoBehaviour
{
    private GameObject player;

    public CinemachineVirtualCamera virtualCamera;

    private CarController carController;

    private WheelTrailRendererHandler[] wt;
    private WheelParticleHandler[] wp;

    [HideInInspector]
    public bool canEnterCar = false;
    private bool pressed = false;

    private void Awake()
    {
        player = GameObject.Find("Player");

        carController = gameObject.GetComponent<CarController>();
        carController.enabled = false;

        wt = gameObject.GetComponentsInChildren<WheelTrailRendererHandler>();
        wp = gameObject.GetComponentsInChildren<WheelParticleHandler>();

        foreach (WheelTrailRendererHandler _wt in wt)
        {
            _wt.enabled = false;
        }

        foreach (WheelParticleHandler _wp in wp)
        {
            _wp.enabled = false;
        }
    }

    private void Update()
    {
        if (canEnterCar && Input.GetKeyDown(KeyCode.F) && carController.velocityVsUp < 0.2f && carController.velocityVsUp > -0.2f)
        {
            carController.enabled = !carController.enabled;
            virtualCamera.m_Follow = gameObject.transform;

            foreach (WheelTrailRendererHandler _wt in wt)
            {
                _wt.enabled = !_wt.enabled;
            }

            foreach (WheelParticleHandler _wp in wp)
            {
                _wp.enabled = !_wp.enabled;
            }

            player.GetComponent<Rigidbody2D>().isKinematic = true;
            player.GetComponent<PlayerController>().enabled = !player.GetComponent<PlayerController>().enabled;
            player.transform.parent = gameObject.transform;
            player.SetActive(false);

            if (pressed && Input.GetKeyDown(KeyCode.F))
            {
                player.transform.parent = null;
                virtualCamera.m_Follow = player.transform;
                player.SetActive(true);
                pressed = false;
                return;
            }
            pressed = true;
        }
    }
}
