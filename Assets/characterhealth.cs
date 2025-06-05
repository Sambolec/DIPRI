using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public HealthBarUI healthBarUI;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        HealthManager.Instance.OnHealthChanged.AddListener(UpdateHealthBar);
        HealthManager.Instance.OnDeath.AddListener(Die);
        UpdateHealthBar(HealthManager.Instance.CurrentHealth);
    }

    void OnDestroy()
    {
        if (HealthManager.Instance != null)
        {
            HealthManager.Instance.OnHealthChanged.RemoveListener(UpdateHealthBar);
            HealthManager.Instance.OnDeath.RemoveListener(Die);
        }
    }

    public void TakeDamage(int amount)
    {
        HealthManager.Instance.TakeDamage(amount);
    }

    void UpdateHealthBar(int health)
    {
        if (healthBarUI != null)
            healthBarUI.SetHealth(health);
    }

    void Die()
    {
        if (animator != null)
            animator.SetTrigger("isDead");
        // Optionally disable movement, attacks, etc.
    }
}
