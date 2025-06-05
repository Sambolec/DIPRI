using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ailogika : MonoBehaviour
{
    [Header("Target")]
    public Transform player;
    
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
        // Always check for the current player (handles character switching)
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        bool playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        bool playerInAttack = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInAttack && playerInSight)
        {
            AttackPlayer();
        }
        else if (playerInSight)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        // FIXED: Sync animations with actual agent movement
        UpdateAnimations();
    }

    void UpdateAnimations()
    {
        if (animator == null || agent == null) return;

        // Check if agent is actually moving
        bool isMoving = agent.velocity.magnitude > 0.1f && agent.remainingDistance > agent.stoppingDistance;
        float speed = agent.velocity.magnitude;

        // Only set walking/running if actually moving
        if (isMoving)
        {
            // If moving fast (chasing), set running. If slow (patrolling), set walking
            if (speed > patrolSpeed + 0.5f)
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
            }
        }
        else
        {
            // Not moving - idle state
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
    }

    void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            agent.isStopped = true;
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

    void ChasePlayer()
    {
        if (player == null) return;
        
        agent.speed = chaseSpeed;
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    void AttackPlayer()
    {
        if (player == null) return;
        
        agent.isStopped = true;
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);

        if (!animator.GetBool("isAttacking") && !isResettingAttack && Time.time - lastAttackTime >= attackCooldown)
        {
            animator.SetBool("isAttacking", true);

            if (player.TryGetComponent(out CharacterHealth playerHealth))
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log($"Dealt {attackDamage} damage to player!");
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
