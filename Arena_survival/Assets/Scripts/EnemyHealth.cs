
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 30;

    private int currentHealth;
    private EnemySpawner spawner;
    private EnemyHealthBar healthBar;

    void Awake()
    {
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<EnemyHealthBar>(true);
    }

    void OnEnable()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.UpdateHealth(1f);
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0)
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log("Enemy Health: " + currentHealth);

        if (healthBar != null)
        {
            healthBar.UpdateHealth((float)currentHealth / maxHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (spawner != null)
        {
            spawner.EnemyDied();
        }

        gameObject.SetActive(false);
    }

    public void SetSpawner(EnemySpawner enemySpawner)
    {
        spawner = enemySpawner;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;


    }
}
