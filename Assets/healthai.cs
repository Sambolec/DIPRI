using UnityEngine;
using UnityEngine.AI;

public class healthai : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    private bool isDead = false;
    private ailogika enemyAI; // Changed from EnemyAI to ailogika
    private NavMeshAgent agent;

    void Awake()
    {
        animator = GetComponent<Animator>();
        enemyAI = GetComponent<ailogika>(); // Changed from GetComponent<EnemyAI>()
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
        animator.SetTrigger("isDead");

        if (enemyAI != null)
            enemyAI.enabled = false;
        if (agent != null)
            agent.enabled = false;

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Destroy(gameObject, 5f);
    }
}
