using UnityEngine;

public class siljci_grote : MonoBehaviour
{
    [Header("Kretanje objekta")]
    public float speed = 1.0f;
    public float topY = 5.0f;
    public float bottomY = 0.0f;
    public float waitAtTop = 1.0f;
    public float waitAtBottom = 1.0f;

    [Header("Teleportacija igrača")]
    public Transform targetPosition; // Povuci objekt s ciljnom pozicijom
    public GameObject player;        // Povuci Player objekt

    private bool movingUp = true;
    private bool waiting = false;
    private float waitTimer = 0f;

    void Update()
    {
        // Logika za kretanje objekta gore-dolje
        if (waiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                waiting = false;
                movingUp = !movingUp;
            }
            return;
        }

        Vector3 position = transform.position;

        if (movingUp)
        {
            position.y += speed * Time.deltaTime;
            if (position.y >= topY)
            {
                position.y = topY;
                waiting = true;
                waitTimer = waitAtTop;
            }
        }
        else
        {
            position.y -= speed * Time.deltaTime;
            if (position.y <= bottomY)
            {
                position.y = bottomY;
                waiting = true;
                waitTimer = waitAtBottom;
            }
        }

        transform.position = position;
    }

    void OnTriggerEnter(Collider other)
    {
        // Provjeri je li to igrač i postoje li reference
        if (other.gameObject == player && targetPosition != null)
        {
            // Teleportiraj igrača na ciljnu poziciju
            player.transform.position = targetPosition.position;

            // Ovdje možeš dodati dodatne efekte (npr. zvuk, particle efekte)
        }
    }
}
