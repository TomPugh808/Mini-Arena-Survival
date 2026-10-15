
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 30;
    public bool isDead  = false;
    public EnemyHealthBar healthBar;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealth(1f);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealth((float)currentHealth / maxHealth);

        Debug.Log("Enemy Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
