using UnityEngine;

public class ArcherNPC : MonoBehaviour
{
    public GameObject arrowPrefab;         // Assign in Inspector
    public Transform arrowSpawnPoint;      // Assign in Inspector
    public Transform target;               // Assign player Transform in Inspector
    public float shootInterval = 2f;       // Time between arrows
    public float arrowSpeed = 15f;

    private float shootTimer;

    void Update()
    {
        if (target == null) return;

        // Face the player (horizontal only)
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        if (lookPos != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(lookPos);

        // Shoot at intervals
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            ShootArrow();
            shootTimer = 0f;
        }
    }

    void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        if (rb != null && target != null)
        {
            Vector3 dir = (target.position + Vector3.up) - arrowSpawnPoint.position; // Aim at chest
            rb.linearVelocity = dir.normalized * arrowSpeed;
        }
    }
}
