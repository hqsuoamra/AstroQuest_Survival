using UnityEngine;
using UnityEngine.UI;

namespace THCHealthSystem
{
    public class THCHealth : MonoBehaviour
    {
        public int maxHealth = 7; // Maximum health of the Enemy
        private int currentHealth;
        private AudioSource audioSource; // Reference to the AudioSource component

        public AudioClip damageSound; // Sound to play when the enemy takes damage
        public AudioClip deathSound; // Sound to play when the enemy dies

        public THCHealthBar thcHealthBar;

        [SerializeField] private GameObject collectiblePrefab; // Assign the collectible prefab in the inspector

        void Start()
        {
            currentHealth = maxHealth; // Initialize the enemy's health
            thcHealthBar.SetMaxHealth(maxHealth); // Initialize the health bar

            audioSource = GetComponent<AudioSource>(); // Get the AudioSource component

            if (audioSource == null)
            {
                Debug.LogError("AudioSource component not found on the enemy object.");
            }
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            Debug.Log("Enemy took damage. Current health: " + currentHealth);
            thcHealthBar.SetHealth(currentHealth);

            // Play damage sound
            if (audioSource != null && damageSound != null)
            {
                audioSource.PlayOneShot(damageSound);
            }
            else
            {
                Debug.LogWarning("Damage sound or AudioSource is not assigned.");
            }

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            Debug.Log("Enemy died. Removing from scene...");

            // Play death sound
            if (audioSource != null && deathSound != null)
            {
                audioSource.PlayOneShot(deathSound);
            }
            else
            {
                Debug.LogWarning("Death sound or AudioSource is not assigned.");
            }

            // Instantiate the collectible at the enemy's position
            if (collectiblePrefab != null)
            {
                GameObject collectible = Instantiate(collectiblePrefab, transform.position, Quaternion.identity);
                
                // Adjust the position of the collectible relative to the enemy
                collectible.transform.position += new Vector3(0, 1, 0); // Example offset (adjust as needed)
                
                // Adjust the size of the collectible
                collectible.transform.localScale = new Vector3(1, 1, 1); // Example size (adjust as needed)
            }
            else
            {
                Debug.LogError("Collectible prefab is not assigned.");
            }

            // Hide the enemy and health bar
            gameObject.SetActive(false);
            if (thcHealthBar != null)
            {
                thcHealthBar.gameObject.SetActive(false);
            }

            // Notify THC6_ctrl that the enemy is dead
            THC6_ctrl ctrlScript = GetComponent<THC6_ctrl>();
            if (ctrlScript != null)
            {
                ctrlScript.OnDeath();
            }

            // Additional death logic if necessary
        }
    }
}
