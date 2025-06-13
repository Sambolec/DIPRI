using UnityEngine;

public class prati_animaciju : MonoBehaviour
{
    private BoxCollider boxCollider;
    private Vector3 zadnjaPozicija;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        zadnjaPozicija = transform.position;
    }

    void Update()
    {
        // Ako se objekt pomaknuo, ažuriraj collider
        if (transform.position != zadnjaPozicija)
        {
            boxCollider.enabled = false;
            boxCollider.enabled = true;
            zadnjaPozicija = transform.position;
        }
    }
}
