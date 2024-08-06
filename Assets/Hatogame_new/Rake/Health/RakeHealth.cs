using UnityEngine;
using UnityEngine.UI;

public class RakeHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public RakeHealthBar rakeHealthBar;

    void Start()
    {
        currentHealth = maxHealth;
        rakeHealthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        rakeHealthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GetComponent<RakeEnemy>().Die();
        // Additional death logic if necessary
    }
}
