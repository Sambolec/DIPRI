using UnityEngine;
using System.Collections.Generic;

public class propadne_dole : MonoBehaviour
{
    [Header("Dijelovi karaktera")]
    public List<Transform> playerParts; // Dodaj sve dijelove karaktera ovdje u Inspectoru

    [Header("Spawn point")]
    public Transform spawnPoint; // Odredi gdje se karakter vraća

    void OnTriggerStay(Collider other)
    {
        foreach (var part in playerParts)
        {
            if (other.transform == part)
            {
                foreach (var p in playerParts)
                {
                    p.position = spawnPoint.position;
                }
                break;
            }
        }
    }
}
