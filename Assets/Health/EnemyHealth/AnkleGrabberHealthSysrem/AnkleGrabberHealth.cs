using UnityEngine;
using UnityEngine.UI;
using AnkleGrabberHealthScripts; // Add this line to reference the correct namespace

public class AnkleGrabberHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private AudioSource audioSource;
    public AudioClip damageSound;
    public AudioClip deathSound;
    public AnkleGrabberHealthbar ankleGrabberHealthbar;
    [SerializeField] private GameObject collectiblePrefab;

    void Start()
    {
        currentHealth = maxHealth;
        ankleGrabberHealthbar.SetMaxHealth(maxHealth);
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on the AnkleGrabber object.");
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("AnkleGrabber took damage. Current health: " + currentHealth);

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

        ankleGrabberHealthbar.SetHealth(currentHealth);
    }

    private void Die()
    {
        Debug.Log("AnkleGrabber died. Removing from scene...");

        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
        else
        {
            Debug.LogWarning("Death sound or AudioSource is not assigned.");
        }

        if (collectiblePrefab != null)
        {
            GameObject collectible = Instantiate(collectiblePrefab, transform.position, Quaternion.identity);
            collectible.transform.position += new Vector3(0, 1, 0);
            collectible.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            Debug.LogError("Collectible prefab is not assigned.");
        }

        gameObject.SetActive(false);
        if (ankleGrabberHealthbar != null)
        {
            ankleGrabberHealthbar.gameObject.SetActive(false);
        }
    }
}
