using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent zom1;

    public float sightRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public int damage = 10; // damage dealt to player
    public float patrolRadius = 10f;

    public float maxHealth = 50f;
    private float currentHealth;

    private bool playerInSightRange;
    private bool playerInAttackRange;
    private bool alreadyAttacked;

    private Vector3 patrolPoint;
    private bool patrolPointSet;

    private PlayerHealth playerHealth;   // <-- reference to your player health script

    void Start()
    {
        zom1 = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player != null)
            playerHealth = player.GetComponent<PlayerHealth>();

        // FIX: smoother collisions between enemies
        zom1.radius = 0.55f; // smaller collision bubble
        zom1.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
        zom1.avoidancePriority = Random.Range(30, 70); // spreads movement differences
        zom1.autoBraking = false;
    }



    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        playerInSightRange = distance <= sightRange;
        playerInAttackRange = distance <= attackRange;

        if (!playerInSightRange && !playerInAttackRange) Patrol();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    void Patrol()
    {
        if (!patrolPointSet)
            SearchPatrolPoint();

        if (patrolPointSet)
            zom1.SetDestination(patrolPoint);

        Vector3 distanceToPoint = transform.position - patrolPoint;

        if (distanceToPoint.magnitude < 1f)
            patrolPointSet = false;
    }

    void SearchPatrolPoint()
    {
        float randomZ = Random.Range(-patrolRadius, patrolRadius);
        float randomX = Random.Range(-patrolRadius, patrolRadius);

        Vector3 randomPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 2f, NavMesh.AllAreas))
        {
            patrolPoint = hit.position;
            patrolPointSet = true;
        }
    }

    void ChasePlayer()
    {
        zom1.SetDestination(player.position);
    }

    void AttackPlayer()
    {
        zom1.SetDestination(transform.position); // stop moving

        Vector3 lookDir = (player.position - transform.position).normalized;
        lookDir.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDir);

        // Attack logic
        if (!alreadyAttacked)
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);   // <-- THIS DEALS DAMAGE
            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
