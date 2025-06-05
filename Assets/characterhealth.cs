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
        // Trigger death animation
        if (animator != null)
            animator.SetTrigger("isDead");

        // Disable movement
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement != null)
            playerMovement.canMove = false;

        // Stop physics-based movement
        CharacterController controller = GetComponent<CharacterController>();
        if (controller != null)
            controller.enabled = false;

        // Optional: Disable attacks/colliders
        // GetComponent<Collider>().enabled = false;
        // GetComponent<PlayerAttack>().enabled = false;
    }
}
