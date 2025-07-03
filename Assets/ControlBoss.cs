using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public int attackDamage = 30;

    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private float attackTimer = 0f;
    private bool isDead = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead || player == null) return;

        // Move towards player
        agent.SetDestination(player.position);

        // Animate movement (Idle/Chase)
        bool isMoving = agent.velocity.magnitude > 0.1f && agent.remainingDistance > agent.stoppingDistance;
        animator.SetBool("IsMoving", isMoving);

        // Face the player
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        if (direction.magnitude > 0.01f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }

        // Attack logic
        float distance = Vector3.Distance(transform.position, player.position);
        attackTimer += Time.deltaTime;
        if (distance <= attackRange && attackTimer >= attackCooldown)
        {
            animator.SetTrigger("Attack");
            attackTimer = 0f;
        }
    }

    // Call this from an animation event at the hit frame of your attack animation
    public void DealDamage()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position + transform.forward * attackRange * 0.5f, 1f);
        foreach (var hit in hits)
        {
            var charHealth = hit.GetComponent<CharacterHealth>();
            if (charHealth != null)
            {
                charHealth.TakeDamage(attackDamage);
            }
        }
    }

    // Call this from BossHealth when boss dies
    public void Die()
    {
        isDead = true;
        agent.isStopped = true;
        animator.SetBool("IsMoving", false);
        animator.SetTrigger("Die");
    }
}
