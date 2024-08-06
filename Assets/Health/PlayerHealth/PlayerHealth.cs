using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using HealthScripts;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10; // Maximum health of the player
    private int currentHealth;
    private AudioSource audioSource; // Reference to the AudioSource component
    
    public AudioClip damageSound; // Sound to play when the player takes damage
    public AudioClip deathSound; // Sound to play when the player dies

    public HealthBar healthBar; // Reference to the HealthBar script

    void Start()
    {
        currentHealth = maxHealth; // Initialize the player's health
        healthBar.SetMaxHealth(maxHealth); // Initialize the health bar
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

        healthBar.SetHealth(currentHealth); // Update the health bar
    }

    public void RestoreHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log("Player restored health. Current health: " + currentHealth);
        healthBar.SetHealth(currentHealth); // Update the health bar
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
