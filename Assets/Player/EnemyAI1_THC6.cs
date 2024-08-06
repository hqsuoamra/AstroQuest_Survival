using UnityEngine;
using UnityEngine.AI;

public class EnemyAI_THC6 : MonoBehaviour
{
    public Transform player;               // Reference to the player
    public NavMeshAgent agent;             // Reference to the NavMeshAgent component
    public float speed = 2.0f;             // Speed of the enemy
    public float attackRange = 1.0f;       // Range within which the enemy can attack
    public float attackCooldown = 1.5f;    // Time between attacks
    public float wanderRadius = 3.0f;      // Radius for random wandering
    public float detectionRange = 25.0f;   // Detection range for the player
    public int damage = 1;                 // Damage dealt to the player
    public AudioClip attackSound;          // Sound effect for the attack
    public AudioClip idleSound;            // Sound effect for idling

    private float lastAttackTime = 0.0f;   // Time since last attack
    private AudioSource audioSource;       // Reference to the AudioSource component
    private bool isIdlePlaying = false;    // Track if idle sound is playing

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component not found on " + gameObject.name);
        }
        else
        {
            agent.speed = speed; // Set the speed of the NavMeshAgent
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (!agent.isOnNavMesh)
        {
            Debug.LogWarning("Agent is not on the NavMesh.");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (distanceToPlayer <= attackRange)
            {
                Attack();
            }
            else
            {
                agent.SetDestination(player.position);
                StopIdleSound();
            }
        }
        else
        {
            Wander();
            PlayIdleSound();
        }
    }

    void Attack()
    {
        if (Time.time > lastAttackTime + attackCooldown)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
            lastAttackTime = Time.time;

            // Play attack sound
            if (attackSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(attackSound);
            }
        }
    }

    void Wander()
    {
        if (!agent.hasPath)
        {
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1);
            Vector3 finalPosition = hit.position;
            agent.SetDestination(finalPosition);
        }
    }

    void PlayIdleSound()
    {
        if (idleSound != null && audioSource != null && !isIdlePlaying)
        {
            audioSource.clip = idleSound;
            audioSource.loop = true;
            audioSource.Play();
            isIdlePlaying = true;
        }
    }

    void StopIdleSound()
    {
        if (audioSource != null && isIdlePlaying)
        {
            audioSource.Stop();
            isIdlePlaying = false;
        }
    }
}
