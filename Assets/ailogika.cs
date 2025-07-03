using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ailogika : MonoBehaviour
{
    [Header("Targets")]
    public Transform[] players = new Transform[2]; // Povuci oba playera ovdje

    [Header("Detection")]
    public float sightRange = 10f;
    public float attackRange = 2f;
    public LayerMask whatIsPlayer;

    [Header("Combat")]
    public int attackDamage = 25;
    public float attackCooldown = 1.5f;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;

    private NavMeshAgent agent;
    private Animator animator;
    private float lastAttackTime;
    private int currentPatrolPoint = 0;
    private bool isResettingAttack = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Transform targetPlayer = GetClosestPlayer();
        if (targetPlayer == null)
        {
            Patrol();
            return;
        }

        bool playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        bool playerInAttack = Vector3.Distance(transform.position, targetPlayer.position) <= attackRange;

        if (playerInAttack && playerInSight)
        {
            AttackPlayer(targetPlayer);
        }
        else if (playerInSight)
        {
            ChasePlayer(targetPlayer);
        }
        else
        {
            Patrol();
        }
    }

    Transform GetClosestPlayer()
    {
        Transform closest = null;
        float minDist = Mathf.Infinity;
        foreach (Transform t in players)
        {
            if (t == null) continue;
            float dist = Vector3.Distance(transform.position, t.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = t;
            }
        }
        return closest;
    }

    void Patrol()
    {
        animator.SetBool("isWalking", true);
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);

        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            agent.isStopped = true;
            animator.SetBool("isWalking", false);
            return;
        }

        agent.speed = patrolSpeed;
        agent.isStopped = false;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolPoint].position);
        }
        else if (agent.destination != patrolPoints[currentPatrolPoint].position)
        {
            agent.SetDestination(patrolPoints[currentPatrolPoint].position);
        }
    }

    void ChasePlayer(Transform target)
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", true);
        animator.SetBool("isAttacking", false);

        agent.speed = chaseSpeed;
        agent.isStopped = false;
        agent.SetDestination(target.position);
    }

    void AttackPlayer(Transform target)
    {
        agent.isStopped = true;
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);

        if (!animator.GetBool("isAttacking") && !isResettingAttack && Time.time - lastAttackTime >= attackCooldown)
        {
            animator.SetBool("isAttacking", true);

            // Ako player ima Health skriptu, primijeni štetu
            var health = target.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(attackDamage);
                Debug.Log($"Dealt {attackDamage} damage to {target.name}!");
            }
            lastAttackTime = Time.time;
            StartCoroutine(ResetAttackState());
        }
    }

    IEnumerator ResetAttackState()
    {
        isResettingAttack = true;
        yield return new WaitForSeconds(attackCooldown * 0.8f);
        animator.SetBool("isAttacking", false);
        isResettingAttack = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            Gizmos.color = Color.blue;
            for (int i = 0; i < patrolPoints.Length; i++)
            {
                if (patrolPoints[i] != null)
                {
                    Gizmos.DrawSphere(patrolPoints[i].position, 0.3f);
                    if (i < patrolPoints.Length - 1 && patrolPoints[i + 1] != null)
                        Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
                    else if (patrolPoints[0] != null)
                        Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[0].position);
                }
            }
        }
    }
}