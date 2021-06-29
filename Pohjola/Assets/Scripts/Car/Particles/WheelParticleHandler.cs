using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelParticleHandler : MonoBehaviour
{
    private float particleEmmissionRate = 0;

    private CarController carController;

    private ParticleSystem particleSystemSmoke;
    private ParticleSystem.EmissionModule particleSystemEmissionModule;

    private void Awake()
    {
        carController = GetComponentInParent<CarController>();

        particleSystemSmoke = GetComponent<ParticleSystem>();
        particleSystemEmissionModule = particleSystemSmoke.emission;

        particleSystemEmissionModule.rateOverTime = 0;
    }

    private void Update()
    {
        // Reduce particles over time
        particleEmmissionRate = Mathf.Lerp(particleEmmissionRate, 0, Time.deltaTime * 5);
        particleSystemEmissionModule.rateOverTime = particleEmmissionRate;

        if (carController.IsTiresScreeching(out float lateralVelocity, out bool isBraking))
        {
            // If tires are screeching, emit smoke. If car is braking then emit some more smoke
            if (isBraking)
                particleEmmissionRate = 30;
            // If player is drifting then emit smoke based on how much the player is drifting
            else particleEmmissionRate = Mathf.Abs(lateralVelocity) * 2;
        }     
    }
}
