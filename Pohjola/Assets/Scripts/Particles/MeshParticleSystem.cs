using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshParticleSystem : MonoBehaviour
{
    // Max amount of quads in a single mesh
    private const int MAX_QUAD_AMOUNT = 15000;

    // Set in the Editor using Pixel values
    [System.Serializable]
    public struct ParticleUVPixels
    {
        public Vector2Int uv00Pixels;
        public Vector2Int uv11Pixels;
    }
    [SerializeField] private ParticleUVPixels[] particleUVPixelsArray;

    // Holds normalized values texture UV Coordinates
    private struct UVCoords
    {
        public Vector2 uv00;
        public Vector2 uv11;
    }
    private UVCoords[] uVCoordsArray;

    // Reference to the mesh
    private Mesh mesh;

    // Values to make the mesh
    private Vector3[] vertices;
    private Vector2[] uv;
    private int[] triangles;

    // Some magic number
    private int quadIndex;

    private void Awake()
    {
        mesh = new Mesh();

        vertices = new Vector3[4 * MAX_QUAD_AMOUNT];
        uv = new Vector2[4 * MAX_QUAD_AMOUNT];
        triangles = new int[6 * MAX_QUAD_AMOUNT];

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        GetComponent<MeshFilter>().mesh = mesh;

        // Some super great magic happens here. No idea what this does but we'll just let it do its own thing:)
        Material material = GetComponent<MeshRenderer>().material;
        Texture mainTexture = material.mainTexture;
        int textureWidth = mainTexture.width;
        int textureHeight = mainTexture.height;

        List<UVCoords> uvCoordsList = new List<UVCoords>();
        foreach (ParticleUVPixels particleUVPixels in particleUVPixelsArray)
        {
            UVCoords uvCoords = new UVCoords
            {
                uv00 = new Vector2((float)particleUVPixels.uv00Pixels.x / textureWidth, (float)particleUVPixels.uv00Pixels.y / textureHeight),
                uv11 = new Vector2((float)particleUVPixels.uv11Pixels.x / textureWidth, (float)particleUVPixels.uv11Pixels.y / textureHeight)
            };
            uvCoordsList.Add(uvCoords);
        }
        uVCoordsArray = uvCoordsList.ToArray();
    }

    public int AddQuad(Vector3 position, float rotation, Vector3 quadSize, bool skewed, int uvIndex)
    {
        if (quadIndex >= MAX_QUAD_AMOUNT) return 0;

        UpdateQuad(quadIndex, position, rotation, quadSize, skewed, uvIndex);

        int spawnedQuadIndex = quadIndex;
        quadIndex++;

        return spawnedQuadIndex;
    }

    public void UpdateQuad(int quadIndex, Vector3 position, float rotation, Vector3 quadSize, bool skewed, int uvIndex) 
    {
        int vIndex = quadIndex * 4;
        int vIndex0 = vIndex;
        int vIndex1 = vIndex + 1;
        int vIndex2 = vIndex + 2;
        int vIndex3 = vIndex + 3;

        if (skewed)
        {
            vertices[vIndex0] = position + Quaternion.Euler(0, 0, rotation - 180) * new Vector3(-quadSize.x, -quadSize.y);
            vertices[vIndex1] = position + Quaternion.Euler(0, 0, rotation - 270) * new Vector3(-quadSize.x, +quadSize.y);
            vertices[vIndex2] = position + Quaternion.Euler(0, 0, rotation - 0) * new Vector3(+quadSize.x, +quadSize.y);
            vertices[vIndex3] = position + Quaternion.Euler(0, 0, rotation - 90) * new Vector3(+quadSize.x, -quadSize.y);
        }
        else
        {
            vertices[vIndex0] = position + Quaternion.Euler(0, 0, rotation - 180) * quadSize;
            vertices[vIndex1] = position + Quaternion.Euler(0, 0, rotation - 270) * quadSize;
            vertices[vIndex2] = position + Quaternion.Euler(0, 0, rotation - 0) * quadSize;
            vertices[vIndex3] = position + Quaternion.Euler(0, 0, rotation - 90) * quadSize;
        }


        // UV
        UVCoords uvCoords = uVCoordsArray[uvIndex];
        uv[vIndex0] = uvCoords.uv00;
        uv[vIndex1] = new Vector2(uvCoords.uv00.x, uvCoords.uv11.y);
        uv[vIndex2] = uvCoords.uv11;
        uv[vIndex3] = new Vector2(uvCoords.uv11.x, uvCoords.uv00.y);

        // Create triangles
        int tIndex = quadIndex * 6;

        triangles[tIndex + 0] = vIndex0;
        triangles[tIndex + 1] = vIndex1;
        triangles[tIndex + 2] = vIndex2;

        triangles[tIndex + 3] = vIndex0;
        triangles[tIndex + 4] = vIndex2;
        triangles[tIndex + 5] = vIndex3;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
}
