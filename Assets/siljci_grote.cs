using UnityEngine;

public class siljci_grote : MonoBehaviour
{
    [Header("Player Child Objects")]
    public GameObject playerChild1;   // Povuci prvo dijete playera
    public GameObject playerChild2;   // Povuci drugo dijete playera

    [Header("Kretanje Platforme")]
    public float speed = 1.0f;        // Brzina kretanja
    public float topY = 5.0f;         // Gornja granica (Y)
    public float bottomY = 0.0f;      // Donja granica (Y)
    public float waitAtTop = 1.0f;    // Pauza na vrhu
    public float waitAtBottom = 1.0f; // Pauza na dnu

    [Header("Teleportacija")]
    public Transform targetPosition;  // Povuci spawn/ciljnu poziciju

    private bool movingUp = true;
    private bool waiting = false;
    private float waitTimer = 0f;

    void Update()
    {
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
        if ((other.gameObject == playerChild1 || other.gameObject == playerChild2) && targetPosition != null)
        {
            other.gameObject.transform.position = targetPosition.position;
        }
    }
}
