using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public static event Action OnCollected;
    public static int total;

    private PlayerHealth playerHealth; // Reference to PlayerHealth

    void Awake()
    {
        total++;
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>(); // Find the PlayerHealth script
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(90f, Time.time * 100f, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollected?.Invoke();
            if (playerHealth != null)
            {
                playerHealth.RestoreHealth(10); // Restore health
            }
            Destroy(gameObject);
        }
    }
}
