using UnityEngine;
using System.Collections.Generic;

public class SwordDamage : MonoBehaviour
{
    private int damage = 0;
    private Collider swordCollider;
    private HashSet<healthai> hitEnemies = new HashSet<healthai>();

    void Awake()
    {
        swordCollider = GetComponent<Collider>();
        if (swordCollider != null)
            swordCollider.enabled = false;
        else
            Debug.LogError("SwordDamage: No collider found on sword object!");
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Sword collider triggered with: {other.name}"); // Added debug line

        var health = other.GetComponentInParent<healthai>();
        if (health != null && !hitEnemies.Contains(health))
        {
            health.TakeDamage(damage);
            hitEnemies.Add(health);
            Debug.Log($"Sword hit: {other.name} | Dealt {damage} damage.");
        }
    }

    // Call at the start of each attack (via animation event or attack script)
    public void ResetHitEnemies()
    {
        hitEnemies.Clear();
        Debug.Log("Hit enemies list cleared.");
    }

    public void EnableDamage()
    {
        ResetHitEnemies(); // ADDED THIS LINE - Clear the hit list whenever the sword is enabled
        
        if (swordCollider != null)
        {
            swordCollider.enabled = true;
            Debug.Log("Sword collider enabled.");
        }
    }

    public void DisableDamage()
    {
        if (swordCollider != null)
        {
            swordCollider.enabled = false;
            Debug.Log("Sword collider disabled.");
        }
    }
}
