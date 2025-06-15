using UnityEngine;

public class propadne_dole : MonoBehaviour
{
    public GameObject player;        
    public Transform spawnPoint;     

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && spawnPoint != null)
        {
            player.transform.position = spawnPoint.position;
            
        }
    }
}
