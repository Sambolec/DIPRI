using UnityEngine;

public class snaptoterrain : MonoBehaviour
{
    void Start()
    {
        Vector3 pos = transform.position;
        float terrainY = Terrain.activeTerrain.SampleHeight(pos);
        transform.position = new Vector3(pos.x, terrainY, pos.z);
    }
}
