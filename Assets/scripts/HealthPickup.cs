using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Provjeri postoji li HealthManager
        if (HealthManager.Instance == null) return;

        // Ako health nije već pun
        if (HealthManager.Instance.CurrentHealth < HealthManager.Instance.maxHealth)
        {
            HealthManager.Instance.ResetHealth(); // napuni health
            Destroy(gameObject); // uništi pickup
        }
    }
}



