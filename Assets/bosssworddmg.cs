using UnityEngine;

public class BossWeaponDamage : MonoBehaviour
{
    public int damage = 30;
    private bool canDealDamage = false;

    public void EnableDamage() { canDealDamage = true; }
    public void DisableDamage() { canDealDamage = false; }

    private void OnTriggerEnter(Collider other)
    {
        if (!canDealDamage) return;

        // Only damage player
        Health playerHealth = other.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            canDealDamage = false; // Prevent multiple hits per swing
        }
    }
}
