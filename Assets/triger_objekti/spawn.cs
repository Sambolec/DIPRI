using UnityEngine;

public class spawn : MonoBehaviour
{
    public static string enterPoint = "ulazak";

    void Awake()
    {
        Transform spawnPoint = GameObject.Find(spawn.enterPoint)?.transform;
        if (spawnPoint != null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.transform.position = spawnPoint.position;
            player.transform.rotation = spawnPoint.rotation;
        }
        else
        {
            Debug.LogWarning("Spawn point nije pronađen! Provjeri naziv objekta.");
        }
    }
}