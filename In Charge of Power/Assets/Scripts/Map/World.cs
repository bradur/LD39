// Date   : 29.07.2017 12:59
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;
using TiledSharp;

public class World : MonoBehaviour {

    [SerializeField]
    private TextAsset mapFile;

    [SerializeField]
    private Transform worldContainer;

    [SerializeField]
    private Material groundMaterial;

    [SerializeField]
    private TiledMesh meshPrefab;

    private float runningZ = 0f;

    void Start () {
        TmxMap map = new TmxMap(mapFile.text, "unused");
        for (int index = 0; index < map.Layers.Count; index += 1)
        {
            TmxLayer layer = map.Layers[index];
            TiledMesh mesh = Instantiate(meshPrefab);
            mesh.Init(map.Width, map.Height, layer, groundMaterial, runningZ);
            runningZ -= 0.1f;
            mesh.transform.SetParent(worldContainer, false);
            //mesh.transform.position = Vector3.zero;
        }
    }

    void Update () {
    
    }
}
