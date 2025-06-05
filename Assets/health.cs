using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    private bool isDead = false;

    // Reference to the health bar UI script (assign in Inspector)
    public HealthBarUI healthBarUI;

    // Optional: public getter for current health (can help with debugging)
    public int CurrentHealth => currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        // Initialize the health bar to full
        if (healthBarUI != null)
            healthBarUI.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update the health bar
        if (healthBarUI != null)
            healthBarUI.SetHealth(currentHealth);

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        
        // Disable character controller (prevents sinking)
        CharacterController controller = GetComponent<CharacterController>();
        if (controller != null)
            controller.enabled = false;
            
        // Disable main collider (prevents physics issues)
        Collider mainCollider = GetComponent<Collider>();
        if (mainCollider != null)
            mainCollider.enabled = false;
            
        // Disable player movement script (prevents moving while dead)
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement != null)
            playerMovement.enabled = false;
            
        // Disable player attack script (prevents attacking while dead)
        PlayerAttack playerAttack = GetComponent<PlayerAttack>();
        if (playerAttack != null)
            playerAttack.enabled = false;
        
        // Trigger death animation
        if (animator != null)
            animator.SetTrigger("isDead");
    }
}
