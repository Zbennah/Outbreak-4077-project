using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent zom1;

    public float sightRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public float damage = 10f;
    public float patrolRadius = 10f;


    public float maxHealth = 50f;
    private float currentHealth;

    private bool playerInSightRange;
    private bool playerInAttackRange;
    private bool alreadyAttacked;

    private Vector3 patrolPoint;
    private bool patrolPointSet;

    void Start()
    {
        zom1 = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        // Check distances
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
  
        zom1.SetDestination(transform.position);


        Vector3 lookDir = (player.position - transform.position).normalized;
        lookDir.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDir);

    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
