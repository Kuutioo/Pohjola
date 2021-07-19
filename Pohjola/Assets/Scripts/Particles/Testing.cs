using System;
using UnityEngine;

public class Testing : MonoBehaviour
{
    // Reference to the PlayerController
    private PlayerController playerController;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();

        playerController.OnDrinkEmpty += PlayerController_OnDrinkEmpty;
    }

    private void PlayerController_OnDrinkEmpty(object sender, EventArgs e)
    {
        Vector3 quadPosition = new Vector3(0f, 0f);
        Vector3 quadSize = new Vector3(1f, 1f);
        float rotation = 0f;

        EmptyDrinkParticleSystemHandler.Instance.SpawnEmptyDrink(quadPosition, new Vector3(1, 1));

        /*
        int spawnedQuadIndex = AddQuad(quadPosition, rotation, quadSize, true, 0);
        UpdateQuad(spawnedQuadIndex, quadPosition, rotation, quadSize, true, 0);
        */
    }
}
