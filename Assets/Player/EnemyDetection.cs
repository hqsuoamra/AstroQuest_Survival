using UnityEngine;
using System.Collections;

public class EnemyDetection : MonoBehaviour
{
    public float detectionRadius = 5f; // Radius for detecting the player
    public LayerMask playerLayer; // LayerMask to identify the player
    private bool isAttacking = false; // To prevent multiple attacks
    private Animator anim; // Animator for attack animation

    private void Start()
    {
        anim = GetComponent<Animator>(); // Get the Animator component
    }

    private void Update()
    {
        // Detect player within the detection radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);
        if (hitColliders.Length > 0 && !isAttacking)
        {
            StartCoroutine(AttackPlayer(hitColliders[0].gameObject)); // Start attack coroutine
        }
    }

    private IEnumerator AttackPlayer(GameObject player)
    {
        isAttacking = true;
        anim.SetBool("Attack", true); // Trigger the attack animation

        // Wait until the attack animation reaches the damage frame (customize the timing)
        yield return new WaitForSeconds(0.5f); // Wait for half of the animation duration

        // Apply damage to the player
        if (player.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(1); // Apply 1 damage
        }

        //yield return new WaitForSeconds(0.5f); // Wait for the rest of the animation duration
        //anim.SetBool("Attack", false); // Stop the attack animation

        isAttacking = false; // Allow the next attack
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // Visualize detection radius in the editor
    }
}
