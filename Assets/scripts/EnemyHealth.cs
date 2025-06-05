using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} primio {amount} damage! Health: {currentHealth}");
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        // Ovdje možeš animaciju smrti, drop item itd...
        Destroy(gameObject); // Za sada jednostavno ukloni enemy iz scene
    }
}

