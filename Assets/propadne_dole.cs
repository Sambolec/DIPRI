using UnityEngine;

public class propadne_dole : MonoBehaviour
{
    public GameObject playerChild1;   // Povuci prvo dijete playera u Inspectoru
    public GameObject playerChild2;   // Povuci drugo dijete playera u Inspectoru
    public Transform spawnPoint;      // Povuci spawn point u Inspectoru

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject == playerChild1 || other.gameObject == playerChild2) && spawnPoint != null)
        {
            other.gameObject.transform.position = spawnPoint.position;
        }
    }
}
