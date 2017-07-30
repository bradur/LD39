// Date   : 29.07.2017 08:09
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;
using TiledSharp;

public enum LayerType
{
    None,
    Ground,
    WaterPlatform,
    FactoryPlatform
}

[RequireComponent(typeof(Mesh))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TiledMesh : MonoBehaviour
{

    private Mesh mesh;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;

    private Vector3[] vertices;
    private Vector3[] normals;
    private int[] triangles;
    private Vector2[] uv;
    private Vector2[] tiles;

    [SerializeField]
    private int tilesPerSide = 8;
    [SerializeField]
    private float unitSize = 1;

    private float tileSize;
    private TmxMap map;

    private Transform wallContainer;
    private Transform waterContainer;

    [SerializeField]
    private GameObject wallPrefab;

    [SerializeField]
    private GameObject waterPrefab;

    private int width;
    private int height;

    private void Start()
    {

    }

    public void Init(int width, int height, TmxLayer layer, Material material, float zPos)
    {
        this.width = width;
        this.height = height;
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
        meshRenderer.sharedMaterial = material;
        tileSize = unitSize / tilesPerSide;
        CalculateTiles();
        InitMesh(width, height);
        transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
        DrawMesh(width, height, layer);
        UpdateMesh();
        //gameObject.isStatic = true;
    }

    // calculates UV coordinates for each tile
    void CalculateTiles()
    {
        // top to bottom left to right
        tiles = new Vector2[tilesPerSide * tilesPerSide];
        for (int x = 0; x < tilesPerSide; x++)
        {
            for (int z = 0; z < tilesPerSide; z++)
            {
                tiles[x + z * tilesPerSide] = new Vector2(x * tileSize, (tilesPerSide - z - 1) * tileSize);
            }
        }
    }

    void InitMesh(int tileCountX, int tileCountZ)
    {
        int tileCount = tileCountX * tileCountZ;
        int vertexCount = tileCount * 6;
        vertices = new Vector3[vertexCount];
        normals = new Vector3[vertexCount];
        triangles = new int[vertexCount];
        uv = new Vector2[vertexCount];
    }

    void DrawMesh(int tileCountX, int tileCountZ, TmxLayer layer)
    {
        //Vector3 startingPosition = new Vector3(-tileCountX / 2 - unitSize / 2, 0f, -tileCountZ / 2 - unitSize / 2);
        Vector3 startingPosition = new Vector3(-unitSize / 2, 0f, -unitSize / 2);
        int index = 0;
        LayerType layerType = (LayerType)Tools.IntParseFast(layer.Properties["Type"]);
        int numTiles = 0;
        float lowestX = -100f;
        float lowestY = -100f;
        for (int z = 0; z < tileCountZ; z++)
        {
            for (int x = 0; x < tileCountX; x++)
            {
                TmxLayerTile tile = layer.Tiles[(tileCountZ - z - 1) * tileCountX + x];
                float xPos = startingPosition.x + x * unitSize;
                float yPos = startingPosition.z + z * unitSize;
                int tileId = tile.Gid - 1;
                if (tileId == -1)
                {
                    continue;
                }
                lowestX = lowestX == -100f ? tile.X : (lowestX > tile.X ? tile.X : lowestX);
                lowestY = lowestY == -100f ? tile.Y : (lowestY > tile.Y ? tile.Y : lowestY);
                numTiles += 1;
                Vector3 currentPosition = new Vector3(xPos, startingPosition.y, yPos);
                DrawVertex(index + 2, currentPosition);
                DrawVertex(index + 1, currentPosition, unitSize);
                DrawVertex(index, currentPosition, unitSize, unitSize);

                DrawVertex(index + 5, currentPosition);
                DrawVertex(index + 4, currentPosition, unitSize, unitSize);
                DrawVertex(index + 3, currentPosition, 0, unitSize);
                AssignUv(index, tiles[tileId], tileSize);
                /*else if (layerType == LayerType.Water)
                {
                    SpawnWater(tileCountX, tileCountZ, tile.X, tile.Y);
                }*/
                index += 6;
            }

        }
        if (layerType == LayerType.FactoryPlatform || layerType == LayerType.WaterPlatform)
        {
            //SpawnFactoryFloor(tileCountX, tileCountZ, tile.X, tile.Y);
            MeshCollisionHandler meshCollisionHandler = GetComponent<MeshCollisionHandler>();
            meshCollisionHandler.name = string.Format("mch: {0}", layer.Name);
            meshCollisionHandler.Init(numTiles, lowestX, this.height - lowestY, layerType);
        }
    }

    private void SpawnWater(int width, int height, int x, int y)
    {
        GameObject water = Instantiate(waterPrefab);
        water.transform.parent = waterContainer;
        water.transform.position = new Vector3(x, height - y - 1f, 0f);
    }

    private void SpawnFactoryFloor(int width, int height, int x, int y)
    {
        GameObject wall = Instantiate(wallPrefab);
        wall.transform.parent = wallContainer;
        wall.transform.position = new Vector3(x, height - y - 1f, 0f);
    }

    void AssignUv(int index, Vector2 texture, float uSize)
    {
        uv[index + 2] = new Vector2(texture.x, texture.y);
        uv[index + 1] = new Vector2(texture.x + uSize, texture.y);
        uv[index] = new Vector2(texture.x + uSize, texture.y + uSize);
        uv[index + 3] = new Vector2(texture.x, texture.y + uSize);
        uv[index + 5] = uv[index + 2];
        uv[index + 4] = uv[index];
    }

    void DrawVertex(int index, Vector3 position, float x = 0, float z = 0)
    {
        position = new Vector3(position.x + x, position.y, position.z + z);
        vertices[index] = position;
        normals[index] = Vector3.up;
        triangles[index] = index;
    }

    void UpdateMesh()
    {
        mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;

        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;

        vertices = null;
        triangles = null;
        normals = null;
        uv = null;
    }
}

