using UnityEngine;
using System.Collections.Generic;

public class siljci_grote : MonoBehaviour
{
    [Header("Spikes movement")]
    public float topY = 5f;
    public float bottomY = 0f;
    public float moveSpeed = 2f;

    [Header("Player respawn")]
    public List<Transform> playerParts; 
    public Transform spawnPoint;

    private bool movingUp = true;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
        }
    }

    void Update()
    {
        Vector3 pos = transform.position;
        if (movingUp)
        {
            pos.y += moveSpeed * Time.deltaTime;
            if (pos.y >= topY)
            {
                pos.y = topY;
                movingUp = false;
            }
        }
        else
        {
            pos.y -= moveSpeed * Time.deltaTime;
            if (pos.y <= bottomY)
            {
                pos.y = bottomY;
                movingUp = true;
            }
        }
        rb.MovePosition(pos);
    }

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
