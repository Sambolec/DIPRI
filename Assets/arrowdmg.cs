using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage = 20;
    public float lifetime = 5f; // Arrow will self-destruct after 5 seconds
    private bool hasHit = false;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        if (rb != null && rb.linearVelocity.sqrMagnitude > 0.1f)
        {
            // If your mesh tip points X+, use this:
            transform.rotation = Quaternion.LookRotation(rb.linearVelocity) * Quaternion.Euler(0, -90, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;
        hasHit = true;
        var health = other.GetComponentInParent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
