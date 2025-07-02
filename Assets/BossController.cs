using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public enum BossState { PhaseOne, PhaseTwo }
    public BossState currentState = BossState.PhaseOne;

    public float maxHealth = 100f;
    public float currentHealth;
    public Transform player;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float shootInterval = 2f;
    private float shootTimer;

    public float chaseSpeed = 5f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    private float attackTimer;

    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Switch phase based on health
        if (currentHealth <= maxHealth / 2 && currentState == BossState.PhaseOne)
        {
            currentState = BossState.PhaseTwo;
            animator.SetBool("isPhaseTwo", true);
        }

        switch (currentState)
        {
            case BossState.PhaseOne:
                PhaseOneBehavior();
                break;
            case BossState.PhaseTwo:
                PhaseTwoBehavior();
                break;
        }
    }

    void PhaseOneBehavior()
    {
        agent.isStopped = true;
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            animator.SetTrigger("isShooting");
            ShootProjectile();
            shootTimer = 0f;
        }
    }

    void PhaseTwoBehavior()
    {
        agent.isStopped = false;
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                animator.SetTrigger("isAttacking");
                SwordAttack();
                attackTimer = 0f;
            }
        }
    }

    void ShootProjectile()
    {
        Vector3 direction = (player.position - firePoint.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));
        // Add force or velocity to projectile here
    }

    void SwordAttack()
    {
        // Implement sword attack logic (e.g., damage player if in range)
        Debug.Log("Boss attacks with sword!");
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Play death animation, disable boss, etc.
        Debug.Log("Boss defeated!");
    }
}
