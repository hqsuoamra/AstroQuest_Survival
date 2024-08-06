/*using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public int healthRestoreAmount = 10;
    public AudioClip collectSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.RestoreHealth(healthRestoreAmount);

                // Play collect sound if available
                if (collectSound != null)
                {
                    AudioSource.PlayClipAtPoint(collectSound, transform.position);
                }

                // Increment collected items count (implement UI update logic)
                UIManager.Instance.IncrementItemCount();

                // Destroy the item
                Destroy(gameObject);
            }
        }
    }
}
*/