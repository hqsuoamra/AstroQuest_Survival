using UnityEngine;
using UnityEngine.AI;

public class AIFollow : MonoBehaviour
{
    public GameObject player; // The player GameObject
    public float followSpeed = 1.0f; // Speed at which the AI will follow the player
    public float searchSpeed = 1.0f; // Speed at which the AI will search for the player
    public float attachmentDistance = 1.0f; // Distance at which the AI will attach to the player
    public float attackDistance = 1.0f; // Distance at which the AI will attack the player
    public float randomMovementRange = 10.0f; // Range within which the AI will move randomly
    public float farDistance = 15.0f; // Distance beyond which the AI starts moving randomly
    public float navMeshSearchRadius = 100.0f; // Increased radius for searching the nearest NavMesh
    public int damageAmount = 1; // Damage dealt to the player per attack
    public float attackCooldown = 2.0f; // Cooldown time between attacks

    private Vector3 offset; // Offset position when attached to the player
    private bool isAttached = false; // Flag to check if AI is attached to the player
    private bool isAttacking = false; // Flag to check if AI is attacking the player
    private bool isMovingRandomly = false; // Flag to check if AI is moving randomly
    private NavMeshAgent navMeshAgent; // NavMeshAgent for AI movement
    private PlayerHealth playerHealth; // Reference to the player's health script
    private float lastAttackTime; // Timestamp of the last attack

    void Start()
    {
        // Find the player GameObject if not assigned
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // Get reference to the player's health script
        playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth script not found on the player object.");
        }

        // Log initial position
        Debug.Log("Initial AI Position: " + transform.position);

        // Attempt to add the NavMeshAgent component
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
            Debug.Log("NavMeshAgent component added.");
        }

        // Check if the NavMeshAgent is on a NavMesh
        if (!navMeshAgent.isOnNavMesh)
        {
            bool placedOnNavMesh = MoveToNearestNavMesh();
            if (!placedOnNavMesh)
            {
                Debug.LogError("Failed to place AI on a NavMesh. Ensure the NavMesh is baked properly.");
                enabled = false; // Disable this script to prevent further errors
            }
        }

        // Set the initial speed to search speed
        navMeshAgent.speed = searchSpeed;
    }

    void Update()
    {
        // Ensure the NavMeshAgent is on a NavMesh
        if (!navMeshAgent.isOnNavMesh)
        {
            Debug.LogWarning("NavMeshAgent is not on a NavMesh.");
            return;
        }

        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= attachmentDistance)
        {
            // Attach to the player
            if (!isAttached)
            {
                offset = transform.position - player.transform.position;
                isAttached = true;
            }

            // Follow the player with the offset
            transform.position = player.transform.position + offset;
            isAttacking = false;
            isMovingRandomly = false;
            navMeshAgent.ResetPath(); // Stop random movement
        }
        else if (distanceToPlayer <= attackDistance)
        {
            // Attack the player
            if (Time.time > lastAttackTime + attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time;
            }

            // Reset the attachment and random movement
            isAttached = false;
            isMovingRandomly = false;
            navMeshAgent.ResetPath(); // Stop random movement
        }
        else if (distanceToPlayer > farDistance)
        {
            // Move to random spots
            if (!isMovingRandomly || !navMeshAgent.hasPath || navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                MoveToRandomSpot();
                isMovingRandomly = true;
            }

            // Reset the attachment and attack flags
            isAttached = false;
            isAttacking = false;
        }
        else
        {
            // Move towards the player
            isAttached = false;
            isAttacking = false;
            isMovingRandomly = false;
            navMeshAgent.ResetPath(); // Stop random movement
            navMeshAgent.speed = searchSpeed; // Always set the speed to searchSpeed when moving towards the player
            navMeshAgent.SetDestination(player.transform.position);
        }
    }

    private void AttackPlayer()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
            Debug.Log("Attacking the player!");
        }
    }

    private void MoveToRandomSpot()
    {
        Vector3 randomDirection = Random.insideUnitSphere * randomMovementRange;
        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, randomMovementRange, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
        }
    }

    private bool MoveToNearestNavMesh()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, navMeshSearchRadius, NavMesh.AllAreas))
        {
            navMeshAgent.Warp(hit.position);
            Debug.Log("Moved to nearest NavMesh position at: " + hit.position);
            return true;
        }
        else
        {
            Debug.LogError("Unable to find a NavMesh within search radius from position: " + transform.position);
            return false;
        }
    }
}
