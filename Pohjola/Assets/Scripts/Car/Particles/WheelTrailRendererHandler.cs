using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTrailRendererHandler : MonoBehaviour
{
    private CarController carController;
    private TrailRenderer trailRenderer;

    private void Awake()
    {
        // Get components
        carController = GetComponentInParent<CarController>();
        trailRenderer = GetComponent<TrailRenderer>();

        trailRenderer.emitting = false;
    }

    private void Update()
    {
        if (carController.IsTiresScreeching(out float lateralVelocity, out bool isBraking))
            trailRenderer.emitting = true;
        else trailRenderer.emitting = false;
    }
}
