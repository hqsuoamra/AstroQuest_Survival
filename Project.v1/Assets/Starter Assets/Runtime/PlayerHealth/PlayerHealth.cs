using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // Maximum health of the player
    private int currentHealth;
    private AudioSource audioSource; // Reference to the AudioSource component

    public AudioClip damageSound; // Sound to play when the player takes damage
    public AudioClip deathSound; // Sound to play when the player dies

    void Start()
    {
        currentHealth = maxHealth; // Initialize the player's health
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on the player object.");
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player took damage. Current health: " + currentHealth);

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

    private void Die()
    {
        Debug.Log("Player died. Restarting game...");

        // Play death sound
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
        else
        {
            Debug.LogWarning("Death sound or AudioSource is not assigned.");
        }

        // Wait for the death sound to finish before reloading the scene
        if (deathSound != null)
        {
            Invoke("ReloadScene", deathSound.length);
        }
        else
        {
            ReloadScene();
        }
    }

    private void ReloadScene()
    {
        // Load the "Playground" scene
        SceneManager.LoadScene("Playground");
    }
}
