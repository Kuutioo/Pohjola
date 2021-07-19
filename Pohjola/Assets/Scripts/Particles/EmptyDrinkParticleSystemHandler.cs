using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyDrinkParticleSystemHandler : MonoBehaviour
{
    public static EmptyDrinkParticleSystemHandler Instance { get; private set; }

    private MeshParticleSystem meshParticleSystem;

    private List<Single> singleList;

    private void Awake()
    {
        Instance = this;
        meshParticleSystem = GetComponent<MeshParticleSystem>();
        singleList = new List<Single>();
    }

    private void Update()
    {
        foreach (Single single in singleList)
        {
            single.Update();
        }
    }

    public void SpawnEmptyDrink(Vector3 position, Vector3 direction)
    {
        singleList.Add(new Single(position, direction, meshParticleSystem));
    }


    // Represents a single empty drink
    private class Single
    {
        private MeshParticleSystem meshParticleSystem;

        private Vector3 position;
        private Vector3 direction;
        private Vector3 quadSize;

        private int quadIndex;
        private float rotation;
        
        public Single(Vector3 position, Vector3 direction, MeshParticleSystem meshParticleSystem)
        {
            this.position = position;
            this.direction = direction;
            this.meshParticleSystem = meshParticleSystem;

            quadSize = new Vector3(1f, 1f);
            rotation = Random.Range(0, 360f);

            quadIndex = meshParticleSystem.AddQuad(position, rotation, quadSize, true, 0);
        }

        public void Update()
        {
            position += direction * Time.deltaTime;
            rotation += 360f * Time.deltaTime;

            meshParticleSystem.UpdateQuad(quadIndex, position, rotation, quadSize, true, 0);
        }
    }
}
