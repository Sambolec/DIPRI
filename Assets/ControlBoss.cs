using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public float attackRange = 4f;
    public float attackCooldown = 1.5f;
    public int attackDamage = 30;
    public BossWeaponDamage weaponDamage; // Assign in Inspector

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

        agent.SetDestination(player.position);

        bool isMoving = agent.velocity.magnitude > 0.1f && agent.remainingDistance > agent.stoppingDistance;
        animator.SetBool("IsMoving", isMoving);

        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        if (direction.magnitude > 0.01f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }

        float distance = Vector3.Distance(transform.position, player.position);
        attackTimer += Time.deltaTime;
        if (distance <= attackRange && attackTimer >= attackCooldown)
        {
            animator.SetTrigger("Attack");
            attackTimer = 0f;
            // Optionally: StartCoroutine(AttackSequence());
        }
    }

    public void Die()
    {
        isDead = true;
        agent.isStopped = true;
        animator.SetBool("IsMoving", false);
        animator.SetTrigger("Die");
    }
}
