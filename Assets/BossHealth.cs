using UnityEngine;
using UnityEngine.AI;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    private bool isDead = false;
    private ControlBoss bossAI;
    private NavMeshAgent agent;

    void Awake()
    {
        animator = GetComponent<Animator>();
        bossAI = GetComponent<ControlBoss>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        if (animator != null)
            animator.SetTrigger("Die"); // THIS triggers the death animation

        if (bossAI != null)
            bossAI.enabled = false;
        if (agent != null)
            agent.enabled = false;

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Destroy(gameObject, 5f);
    }
}
