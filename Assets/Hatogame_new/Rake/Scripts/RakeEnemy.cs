using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RakeEnemy : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public NavMeshAgent agent;

    public float sightRange = 20f;
    public float attackRange = 2f;
    public float attackInterval = 2f;

    private float nextAttackTime = 0f;
    private bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
        else if (distanceToPlayer <= sightRange)
        {
            ChasePlayer();
        }
        else
        {
            Idle();
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);

        if (agent.velocity.magnitude > 0.5f)
        {
            animator.SetBool("walk", true);
        }
        else
        {
            animator.SetBool("walk", false);
        }
    }

    void Idle()
    {
        agent.SetDestination(transform.position);
        animator.SetBool("walk", false);
    }

    void AttackPlayer()
    {
        agent.SetDestination(transform.position); // Stop moving

        if (Time.time >= nextAttackTime)
        {
            // Randomly choose between attack1 and attack2
            if (Random.Range(0, 2) == 0)
            {
                animator.SetTrigger("attack1");
            }
            else
            {
                animator.SetTrigger("attack2");
            }

            nextAttackTime = Time.time + attackInterval;
        }

        animator.SetBool("walk", false);
    }

    public void TakeDamage()
    {
        if (isDead) return;

        animator.SetTrigger("hit");

        // Assuming there's a health system in place
        // Reduce health here and check for death
        // If dead, call Die()
    }

    public void Die()
    {
        isDead = true;
        animator.SetTrigger("die");
        agent.enabled = false; // Disable NavMeshAgent to stop movement

        // Additional death logic (e.g., remove from scene after animation)
        Destroy(gameObject, 5f); // Destroy after 5 seconds
    }
}
