using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfEnemies = 5;
    public Vector2 spawnAreaSize = new Vector2(10f, 10f);

    void Start()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos = GetRandomSpawnPosition();
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 center = transform.position;

        float x = Random.Range(center.x - spawnAreaSize.x / 2, center.x + spawnAreaSize.x / 2);
        float z = Random.Range(center.z - spawnAreaSize.y / 2, center.z + spawnAreaSize.y / 2);
        float y = 500f; // visoka točka iznad mape

        Vector3 origin = new Vector3(x, y, z);
        RaycastHit hit;

        // Puca ray prema dolje da pronađe tlo
        if (Physics.Raycast(origin, Vector3.down, out hit, 1000f))
        {
            return hit.point;
        }
        else
        {
            // fallback ako ništa ne pogodi
            return new Vector3(x, center.y, z);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, 1, spawnAreaSize.y));
    }
}
