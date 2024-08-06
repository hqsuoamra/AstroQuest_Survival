using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class THC6_ctrl : MonoBehaviour
{
    public Animator animator;
    public int attackDamage = 1; // Amount of damage the enemy deals
    public float attackRange = 1.0f; // Range within which the enemy attacks the player
    public float detectionRange = 25.0f; // Range within which the enemy detects the player
    public float moveSpeed = 2.0f; // Speed at which the enemy moves towards the player

    private bool isIdle = true;
    private bool isWalking = false;
    private bool isAttacking = false;
    private bool isHit = false;
    private bool isDead = false;
    private bool isPlayerInContact = false; // To track player contact

    public float speed = 2.0f;
    public float runSpeed = 5.0f;
    private float r_sp = 0.0f;

    private PlayerHealth playerHealth; // Reference to the PlayerHealth script
    private NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent component
    private GameObject player; // Reference to the player GameObject

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found on the enemy object.");
        }

        // Find the player and get the PlayerHealth component
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
        else
        {
            Debug.LogError("Player with tag 'Player' not found.");
        }

        navMeshAgent.speed = moveSpeed; // Set the NavMeshAgent speed

        // Initialize speeds
        r_sp = runSpeed;
        runSpeed = 1;
    }

    void Update()
    {
        if (isDead)
        {
            return; // Do nothing if dead
        }

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= attackRange && !isHit && !isAttacking)
            {
                Debug.Log("Enemy is attacking.");
                SetAttacking(true);
                SetWalking(false);
                SetIdle(false);
            }
            else if (distanceToPlayer <= detectionRange && !isHit)
            {
                Debug.Log("Enemy is walking towards the player.");
                SetAttacking(false);
                SetWalking(true);
                SetIdle(false);
                navMeshAgent.isStopped = false; // Ensure the agent is moving
                navMeshAgent.SetDestination(player.transform.position); // Move towards the player
            }
            else
            {
                Debug.Log("Enemy is idle.");
                SetAttacking(false);
                SetWalking(false);
                SetIdle(true);
                navMeshAgent.isStopped = true; // Stop the agent when idle
            }
        }
    }

    void SetIdle(bool value)
    {
        isIdle = value;
        if (value)
        {
            animator.SetInteger("moving", 0); // Set moving to 0 for idle
            animator.SetInteger("battle", 0); // Ensure battle is not active
            StartCoroutine(RandomizeIdleAnimation());
        }
    }

    IEnumerator RandomizeIdleAnimation()
    {
        while (isIdle && !isDead)
        {
            animator.SetInteger("moving", 7); // Idle animation
            animator.SetInteger("battle", 0); // Ensure battle is not active
            yield return new WaitForSeconds(Random.Range(1, 2)); // Random delay between 1 and 2 seconds
        }
    }

    void SetWalking(bool value)
    {
        isWalking = value;
        if (value)
        {
            animator.SetInteger("moving", 1); // Set moving to 1 for walking
            animator.SetInteger("battle", 0); // Ensure battle is not active
        }
        else
        {
            animator.SetInteger("moving", 0); // Reset to idle if not walking
        }
    }

    void SetAttacking(bool value)
    {
        isAttacking = value;
        if (value)
        {
            navMeshAgent.isStopped = true; // Stop moving while attacking
            animator.SetInteger("battle", 1); // Set battle to 1 for attacking
            StartCoroutine(RandomizeAttackAnimation());
        }
        else
        {
            animator.SetInteger("battle", 0); // Reset battle state if not attacking
        }
    }

    IEnumerator RandomizeAttackAnimation()
    {
        while (isAttacking && !isDead)
        {
            int[] attackAnimations = { 2, 3, 4, 6 };
            int randomIndex = Random.Range(0, attackAnimations.Length);
            animator.SetInteger("moving", attackAnimations[randomIndex]); // Use attack values
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f)); // Random delay between attacks
        }
    }

    public void OnHit()
    {
        if (!isDead)
        {
            StartCoroutine(RandomizeHitAnimation());
        }
    }

    IEnumerator RandomizeHitAnimation()
    {
        isHit = true;
        for (int i = 0; i < 3; i++)
        {
            animator.SetInteger("battle", 1); // Set battle to 1 for hit
            int n = Random.Range(0, 2);
            if (n == 1)
            {
                animator.SetInteger("moving", 8); // Hit animation variant 1
            }
            else
            {
                animator.SetInteger("moving", 9); // Hit animation variant 2
            }
            yield return new WaitForSeconds(0.5f);
        }

        animator.SetInteger("moving", 5); // Hit animation (final state)
        yield return new WaitForSeconds(0.5f);

        animator.SetInteger("moving", 13); // Additional hit animation
        yield return new WaitForSeconds(0.5f);

        animator.SetInteger("moving", 15); // Final hit animation
        yield return new WaitForSeconds(0.5f);

        isHit = false;
    }

    public void OnDeath()
    {
        isDead = true;
        animator.SetInteger("moving", 14); // Die animation
        if (navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.isStopped = true;
        }
    }

    void DealDamageToPlayer()
    {
        if (playerHealth != null && isPlayerInContact)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    // This method can be called by an animation event to deal damage
    public void OnAttackHit()
    {
        if (isPlayerInContact)
        {
            DealDamageToPlayer();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth = other.GetComponent<PlayerHealth>();
            isPlayerInContact = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInContact = false;
        }
    }
}
