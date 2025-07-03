using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private BossController bossController;
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        bossController = GetComponent<BossController>();
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;
        currentHealth -= amount;
        if (animator != null)
            animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            isDead = true;
            if (bossController != null)
                bossController.Die();
            Destroy(gameObject, 2f); // Wait for death animation
        }
    }
}
